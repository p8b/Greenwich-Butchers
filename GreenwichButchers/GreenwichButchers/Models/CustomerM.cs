using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    [Serializable]
    public class CustomerM : PersonM
    {
        public int CustomerID { get; set; }
        public string Company { get; set; }
        public List<AddressM> AddressList { get; set; }

        /// <summary>
        /// This method is used to get customer records from the database
        /// </summary>
        /// <param name="CusID">This is the Customer id</param>
        /// <param name="ShowAll">true = Show All, false = Use Customer ID</param>
        /// <returns>List of Customer objects</returns>
        internal async Task<List<CustomerM>> GetCustomersAsync(int CusID, bool ShowAll)
        {
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            // Set the current "AddressList" property to a new List of "Address" objects
            var CustomerList = new List<CustomerM>();
                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewCustomer", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        // @AddressInfo = true (Include Address)
                        // @AddressInfo = false (Exclude Address)
                        sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CusID;
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
                                var Cus = new CustomerM
                                {
                                    // Set the product information received to the current
                                    // instance of "Product" class
                                    PersonID = (int)RecReader["Person_Id"],
                                    CustomerID = (int)RecReader["Customer_Id"],
                                    Title = RecReader["person_Title"].ToString().TrimEnd(),
                                    Name = RecReader["person_Name"].ToString().TrimEnd(),
                                    Surname = RecReader["person_Surname"].ToString().TrimEnd(),
                                    Tel = RecReader["person_Tel"].ToString().TrimEnd(),
                                    Email = RecReader["person_Email"].ToString().TrimEnd(),
                                    Company = RecReader["customer_CompanyName"].ToString().TrimEnd(),
                                    AddressList = await new AddressM().GetAddressesAsync(CusID,"CustomerID").ConfigureAwait(false)
                                };
                                CustomerList.Add(Cus);
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
            // Return the current object which has the customer information
            return CustomerList;
        }

        /// <summary>
        /// This method is used to get one customer record
        /// </summary>
        /// <param name="CusID">This is the id of the customer record</param>
        /// <returns> Returns a customer Object</returns>
        internal async Task<CustomerM> GetOneCustomerAsync(int CusID)
        {
            var CusList = await GetCustomersAsync(CusID, false).ConfigureAwait(false);

            // Set the Customer information received to the current
            // instance of "Customer" class
            if (CusList.Count != 0)
            {
                this.CustomerID = CusList[0].CustomerID;
                this.PersonID = CusList[0].PersonID;
                this.Title = CusList[0].Title;
                this.Name = CusList[0].Name;
                this.Surname = CusList[0].Surname;
                this.Tel = CusList[0].Tel;
                this.Email = CusList[0].Email;
                this.Company = CusList[0].Company;
                this.AddressList = CusList[0].AddressList;
                return CusList[0];
            }
            // else return null
            return null;
        }

        /// <summary>
        /// This method is used to search customers by:
        /// Name, Surname, Email or Customer ID
        /// </summary>
        /// <param name="SearchValue">The search value</param>
        /// <returns>List of customer objects</returns>
        internal async Task<List<CustomerM>> SearchCustomerAsync(string SearchValue)
        {
            var ReturnValue = new List<CustomerM>();
            foreach (var cus in await GetCustomersAsync(0, true).ConfigureAwait(false))
            {
                if (cus.CustomerID.ToString().Contains(SearchValue)
                    || cus.Name.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0
                    || cus.Surname.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0
                    || cus.Email.IndexOf(SearchValue, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    ReturnValue.Add(cus);
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// This method is used to add new customer with at least one address
        /// </summary>
        /// <param name="NewCustomer">Accepts an object of customer class </param>
        /// <returns>True of successful otherwise returns false </returns>
        internal async Task<bool> AddCustomerAsync(CustomerM NewCustomer, AddressM NewAddress)
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
                    using (var sqlCommand = new SqlCommand("AddCustomer", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        // Customer "Person" Details
                        sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 5).Value = NewCustomer.Title;
                        sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = NewCustomer.Name;
                        sqlCommand.Parameters.Add("@Surname", SqlDbType.VarChar, 100).Value = NewCustomer.Surname;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = NewCustomer.Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = NewCustomer.Email;
                        sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 130).Value = NewCustomer.Password;
                        sqlCommand.Parameters.Add("@Company", SqlDbType.VarChar, 300).Value = NewCustomer.Company;

                        // Customer default address
                        sqlCommand.Parameters.Add("@AddressName", SqlDbType.VarChar, 100).Value = NewAddress.AddressName;
                        sqlCommand.Parameters.Add("@FirstLineAdd", SqlDbType.VarChar, 200).Value = NewAddress.FirstLine;
                        sqlCommand.Parameters.Add("@SecondLineAdd", SqlDbType.VarChar, 200).Value = NewAddress.SecondLine;
                        sqlCommand.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = NewAddress.City;
                        sqlCommand.Parameters.Add("@PostCode", SqlDbType.VarChar, 8).Value = NewAddress.PostCode;

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
        /// This method is used to Update the current customer object information 
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
                    using (var sqlCommand = new SqlCommand("UpdateCustomer", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                        sqlCommand.Parameters.Add("@Title", SqlDbType.VarChar, 5).Value = Title;
                        sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = Name;
                        sqlCommand.Parameters.Add("@Surname", SqlDbType.VarChar, 100).Value = Surname;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = Email;
                        sqlCommand.Parameters.Add("@Company", SqlDbType.VarChar, 200).Value = Company;

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
        /// This method is used to delete a customer record
        /// </summary>
        /// <param name="CusId">Customer ID</param>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> DeleteCustomerAsync(int CusId)
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
                    using (var sqlCommand = new SqlCommand("RemoveCustomer", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CusId;

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