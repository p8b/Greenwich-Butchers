
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
    public class OrderItemQuoteM : SupplierM
    {
        public int QuoteID { get; set; }
        public decimal QuotePrice { get; set; }
        public string QuoteDeliveryDate { get; set; }


        /// <summary>
        /// This method is used to add or update the Current OrderItemQuote instance
        /// </summary>
        /// <param name="ItemID">The item ID of the current quote</param>
        /// <returns>true if successful else false </returns>
        internal async Task<bool> AddUpdateOrderQuote(int ItemID)
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
                    using (var sqlCommand = new SqlCommand("AddUpdateOrderQuote", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID;
                        sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;
                        sqlCommand.Parameters.Add("@QuotePrice", SqlDbType.Decimal, 10).Value = QuotePrice;
                        sqlCommand.Parameters.Add("@DeliveryDate", SqlDbType.VarChar, 10).Value = QuoteDeliveryDate;


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
        /// This method is used to delete the current OrderItemQuote from the database
        /// </summary>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> DeleteOrderQuoteAsync()
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
                    using (var sqlCommand = new SqlCommand("RemoveOrderQuote", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@QuoteID", SqlDbType.Int).Value = QuoteID;

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

        internal async Task<List<OrderItemQuoteM>> GetOrderItemQuoteAsync(string FilterBy, int IDValue)
        {
            // Create a new generic list of "OrderItemQuoteList" object
            // to hold all the available quotes for the Order item
            var OrderItemQuoteList = new List<OrderItemQuoteM>();

            // Run the following code asynchronously
            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error Handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewOrderQuote", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@IDValue", SqlDbType.Int).Value = IDValue;
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.NChar, 10).Value = FilterBy;

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
                                // Create a new "OrderItemQuoteM" object
                                var Item = new OrderItemQuoteM
                                {
                                    // set the data received from db to the "Item" Object
                                    QuoteID = (int)RecReader["Quote_Id"],
                                    SupplierID = (int)RecReader["Supplier_Id"],
                                    QuotePrice = (decimal)RecReader["quote_Price"],
                                    QuoteDeliveryDate = RecReader["quote_DeliveryDate"].ToString(),
                                    Company = RecReader["supplier_Company"].ToString()
                                };

                                // Add the "Item" object to the "OrderItemQuoteList"
                                OrderItemQuoteList.Add(Item);
                            }
                        }
                    }
                }
                // Catching all the exception
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

            return OrderItemQuoteList;
        }
    }
}