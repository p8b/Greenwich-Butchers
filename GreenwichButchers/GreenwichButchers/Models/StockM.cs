using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    public class StockM : StockLocationM
    {
        public decimal StockQuantity { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// This method is used to view Stock Records with Filtering capabilities
        /// </summary>
        /// <param name="FilterBy">Used to filter by "Product" or "Location" or "Off" (View All)</param>
        /// <param name="Value">The value to be filtered by</param>
        /// <returns>List of stock records received from </returns>
        internal async Task<List<StockM>> GetStockInfoAsync(string FilterBy, string Value)
        {
            // Create a generic List of "Stock" Class
            // to be used as return value of this method
            var StockList = new List<StockM>();

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
                    using (var sqlCommand = new SqlCommand("ViewStock", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@Value", SqlDbType.VarChar, 300).Value = Value;
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 8).Value = FilterBy;

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
                                var Stock = new StockM
                                {
                                    // Set the product information received to the current
                                    // instance of "Product" class
                                    ProductName = RecReader["Product_Name"].ToString().TrimEnd(),
                                    StockQuantity = (decimal)RecReader["Stock_Quantity"],
                                    LocationName = RecReader["StockLocation_Name"].ToString().TrimEnd()
                                };

                                // Add the current object to the list
                                StockList.Add(Stock);
                            }
                        }
                    }

                    // Return the StockList
                    return StockList;
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);

                    // return empty list
                    return StockList;
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            return StockList;
        }


        #region ** Add one Product stock information method

        /// <summary>
        /// This method is used to add new product stocks to the database. If the record already
        /// exists, then the procedure will update the record
        /// </summary>
        /// <param name="ProdName">Name of the Product</param>
        /// <param name="Quantity">Stock Quantity of the product</param>
        /// <param name="StockLocation">Name of the Stock Location</param>
        internal async Task<bool> AddUpdateStockInfoAsync(string ProdName, decimal Quantity, string StockLocation)
        {
            var ReturnValue = false;
            // Adding asynchronous capability
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
                    using (var sqlCommand = new SqlCommand("AddUpdateStock", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = ProdName;
                        sqlCommand.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
                        sqlCommand.Parameters.Add("@StockLocation", SqlDbType.VarChar, 30).Value = StockLocation;

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

        #endregion 

    }
}