using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    public class StockLocationM
    {
        public string LocationName { get; set; }

        /// <summary>
        /// This method is used to receive a list of stock location from the database
        /// </summary>
        /// <returns> All the Stock Location in the database </returns>
        public async Task<List<StockLocationM>> GetStockLocationsAsync()
        {
            // Create a generic List of "StockLocation" Class
            // to be used as return value of this method
            var StockLocationList = new List<StockLocationM>();
            // Run the following code asynchronously
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
                    using (var sqlCommand = new SqlCommand("ViewSockLocations", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = System.Data.CommandType.StoredProcedure
                    })
                    {
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
                                // Create a new object of "Product" class
                                var StockLocation = new StockLocationM
                                {
                                    // Set the product information received to the current
                                    // instance of "Product" class
                                    LocationName = RecReader["StockLocation_Name"].ToString().TrimEnd()
                                };

                                // Add the current object to the list
                                StockLocationList.Add(StockLocation);
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
            return StockLocationList;
        }

        /// <summary>
        /// This method is used to add new stock location to the database
        /// </summary>
        /// <returns>Return "true" if successful else return "false"</returns>
        public async Task<bool> AddStockLocationAsync()
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
                    using (var sqlCommand = new SqlCommand("AddStockLocation", sqlConn)
                    {
                        // Specify the command type as stored procedure

                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@LocationName", SqlDbType.VarChar, 30).Value = LocationName;

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
        /// This method is used to delete stock location records
        /// </summary>
        /// <returns>Return "true" if successful else return "false"</returns>
        public async Task<bool> DeleteStockLocationAsync()
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
                    using (var sqlCommand = new SqlCommand("RemoveStockLocation", sqlConn)
                    {
                        // Specify the command type as stored procedure

                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@LocationName", SqlDbType.VarChar, 30).Value = LocationName;

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
        /// This method is used to update stock location records
        /// </summary>
        /// <returns>Return "true" if successful else return "false"</returns>
        public async Task<bool> UpdateStockLocationAsync(string NewLocationName)
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
                    using (var sqlCommand = new SqlCommand("UpdateStockLocation", sqlConn)
                    {
                        // Specify the command type as stored procedure

                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@FromLocation", SqlDbType.VarChar, 30).Value = LocationName;
                        sqlCommand.Parameters.Add("@ToLocation", SqlDbType.VarChar, 30).Value = NewLocationName;

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