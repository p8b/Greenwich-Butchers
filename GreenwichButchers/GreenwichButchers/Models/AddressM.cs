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
    public class AddressM
    {
        public int AddressID { get; set; }
        public string AddressName { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        /// <summary>
        /// This Method is used to get list of addresses for one customer
        /// </summary>
        /// <returns>List of address objects</returns>
        internal async Task<List<AddressM>> GetAddressesAsync(int FilterValue, string FilterBy)
        {
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);






            // Set the current "AddressList" property to a new List of "Address" objects
            var AddressList = new List<AddressM>();

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
                    using (var sqlCommand = new SqlCommand("ViewCustomerAddress", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@FilterValue", SqlDbType.Int).Value = FilterValue;
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 11).Value = FilterBy;

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
                                var address = new AddressM
                                {
                                    AddressID = (int)RecReader["Address_Id"],
                                    AddressName = RecReader["address_Name"].ToString().TrimEnd(),
                                    FirstLine = RecReader["address_1stLine"].ToString().TrimEnd(),
                                    SecondLine = RecReader["address_2ndLine"].ToString().TrimEnd(),
                                    City = RecReader["address_City"].ToString().TrimEnd(),
                                    PostCode = RecReader["address_PostCode"].ToString().TrimEnd()
                                };
                                AddressList.Add(address);
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

            // Return the current object which has the customer information
            return AddressList;
        }

        /// <summary>
        /// This method is used to update the current instance of address class
        /// </summary>
        /// <returns>True of successful else false</returns>
        internal async Task<bool> UpdateAddressAsync()
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
                    using (var sqlCommand = new SqlCommand("UpdateCustomerAddress", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = AddressID;
                        sqlCommand.Parameters.Add("@AddressName", SqlDbType.VarChar, 100).Value = AddressName;
                        sqlCommand.Parameters.Add("@FirstLineAdd", SqlDbType.VarChar, 200).Value = FirstLine;
                        sqlCommand.Parameters.Add("@SecondLineAdd", SqlDbType.VarChar, 200).Value = SecondLine;
                        sqlCommand.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = City;
                        sqlCommand.Parameters.Add("@PostCode", SqlDbType.VarChar, 8).Value = PostCode;

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
        /// This method is used to add new Address for a customer
        /// </summary>
        /// <param name="CustomerID">Customer ID</param>
        /// <returns>true if successful else false</returns>
        internal async Task<bool> AddNewAddressAsync(int CustomerID)
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
                    using (var sqlCommand = new SqlCommand("AddCustomerAddress", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                        sqlCommand.Parameters.Add("@AddressName", SqlDbType.VarChar, 100).Value = AddressName;
                        sqlCommand.Parameters.Add("@FirstLineAdd", SqlDbType.VarChar, 200).Value = FirstLine;
                        sqlCommand.Parameters.Add("@SecondLineAdd", SqlDbType.VarChar, 200).Value = SecondLine;
                        sqlCommand.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = City;
                        sqlCommand.Parameters.Add("@PostCode", SqlDbType.VarChar, 8).Value = PostCode;

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
        /// This method is used to delete a customer address
        /// </summary>
        /// <param name="AddId">Address ID</param>
        /// <returns>true if successful else false</returns>
        internal async Task<bool> DeleteAddressAsync(int AddId)
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
                    using (var sqlCommand = new SqlCommand("RemoveCustomerAddress", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = AddId;

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