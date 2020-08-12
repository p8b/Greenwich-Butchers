using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    public class EmployeeM : PersonM 
    {
        public int EmployeeID { get; set; }
        public string Position { get; set; }
        public bool InitialEmp { get; set; }

        /// <summary>
        /// This method is used to get Employee records from the database
        /// </summary>
        /// <param name="EmpID">This is the Employee id</param>
        /// <param name="ShowAll">true = Show All, false = Use Employee ID</param>
        /// <returns>List of Employee objects</returns>
        internal async Task<List<EmployeeM>> GetEmployeeAsync(int EmpID, bool ShowAll)
        {
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            
            // Set the current "AddressList" property to a new List of "Address" objects
            var EmployeeList = new List<EmployeeM>();

            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewEmployee", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        // @AddressInfo = true (Include Address)
                        // @AddressInfo = false (Exclude Address)
                        sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmpID;
                        sqlCommand.Parameters.Add("@ShowAll", SqlDbType.Bit).Value = ShowAll;

                        // Open the database connection
                        sqlConn.Open();

                        // Receive all the relevant records by calling the
                        // "ExecuteReader" method of "sqlCommand" Object
                        var RecReader = sqlCommand.ExecuteReader();

                        // Check if the there are any records
                        if (RecReader.HasRows)
                        {
                            // Loop through the records
                            while (RecReader.Read())
                            {
                                var Emp = new EmployeeM
                                {
                                    // Set the product information received to the current
                                    // instance of "Product" class
                                    PersonID = (int)RecReader["Person_Id"],
                                    EmployeeID = (int)RecReader["Employee_Id"],
                                    Title = RecReader["person_Title"].ToString().TrimEnd(),
                                    Name = RecReader["person_Name"].ToString().TrimEnd(),
                                    Surname = RecReader["person_Surname"].ToString().TrimEnd(),
                                    Tel = RecReader["person_Tel"].ToString().TrimEnd(),
                                    Email = RecReader["person_Email"].ToString().TrimEnd(),
                                    Position = RecReader["Position_Name"].ToString().TrimEnd()
                                };
                                EmployeeList.Add(Emp);
                            }
                        }
                    }
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            // Return the current object which has the Employee information
            return EmployeeList;
        }

        /// <summary>
        /// This method is used to get one Employee record
        /// </summary>
        /// <param name="EmpID">This is the id of the Employee record</param>
        /// <returns> Returns a Employee Object</returns>
        internal async Task<EmployeeM> GetOneEmployeeAsync(int EmpID)
        {
            var EmpList = await GetEmployeeAsync(EmpID, false).ConfigureAwait(false);

            // Set the Employee information received to the current
            // instance of "Employee" class
            if (EmpList.Count != 0)
            {
                this.EmployeeID = EmpList[0].EmployeeID;
                this.PersonID = EmpList[0].PersonID;
                this.Title = EmpList[0].Title;
                this.Name = EmpList[0].Name;
                this.Surname = EmpList[0].Surname;
                this.Tel = EmpList[0].Tel;
                this.Email = EmpList[0].Email;

                return EmpList[0];
            }
            // else return null
            return null;
        }

        /// <summary>
        /// This method is used to search Employee by:
        /// Name, Surname, Email or Employee ID
        /// </summary>
        /// <param name="SearchValue">The search value</param>
        /// <returns>List of Employee objects</returns>
        internal async Task<List<EmployeeM>> SearchEmployeeAsync(string SearchValue)
        {
            var ReturnValue = new List<EmployeeM>();
            foreach (var Emp in await GetEmployeeAsync(0, true).ConfigureAwait(false))
            {
                if (Emp.EmployeeID.ToString().Contains(SearchValue)
                    || Emp.Name.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0
                    || Emp.Surname.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0
                    || Emp.Email.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    ReturnValue.Add(Emp);
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// This method is used to add new Employee
        /// </summary>
        /// <param name="NewEmployee">Accepts an object of Employee class </param>
        /// <returns>True of successful otherwise returns false </returns>
        internal async Task<bool> AddEmployeeAsync(EmployeeM NewEmployee)
        {
            var ReturnValue = false;

            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("AddEmployee", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        // Customer "Person" Details
                        sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 5).Value = NewEmployee.Title;
                        sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = NewEmployee.Name;
                        sqlCommand.Parameters.Add("@Surname", SqlDbType.VarChar, 100).Value = NewEmployee.Surname;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = NewEmployee.Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = NewEmployee.Email;
                        sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 130).Value = NewEmployee.Password;
                        sqlCommand.Parameters.Add("@PositionName", SqlDbType.VarChar, 50).Value = NewEmployee.Position;

                        // Open the database connection
                        sqlConn.Open();

                        // Execute the Command
                        sqlCommand.ExecuteNonQuery();
                        ReturnValue = true;
                    }
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            return ReturnValue;
        }

        /// <summary>
        /// This method is used to Update the current Employee object information 
        /// </summary>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> UpdateCustomerInfoAsync()
        {
            var ReturnValue = false;

            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("UpdateEmployee", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeID;
                        sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 5).Value = Title;
                        sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
                        sqlCommand.Parameters.Add("@Surname", SqlDbType.VarChar, 100).Value = Surname;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = Email;
                        sqlCommand.Parameters.Add("@PositionName", SqlDbType.VarChar, 50).Value = Position;

                        // Open the database connection
                        sqlConn.Open();

                        sqlCommand.ExecuteNonQuery();
                        ReturnValue = true;
                    }
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            return ReturnValue;
        }

        /// <summary>
        /// This method is used to delete a Employee record
        /// </summary>
        /// <param name="EmpId">Employee ID</param>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> DeleteEmployeeAsync(int EmpId)
        {
            var ReturnValue = false;

            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("RemoveEmployee", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmpId;

                        // Open the database connection
                        sqlConn.Open();

                        sqlCommand.ExecuteNonQuery();
                        ReturnValue = true;
                    }
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            return ReturnValue;
        }
    }
}