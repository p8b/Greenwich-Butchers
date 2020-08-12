using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    [Serializable]
    public class OrderItemM : ProductM
    {
        public int ItemID { get; set; }
        public decimal ItemPrice { get; set; }
        public OrderItemQuoteM _ItemQuote { get; set; }
        public List<OrderItemQuoteM> _QuoteList { get; set; } = new List<OrderItemQuoteM>();

        internal async Task<List<OrderItemM>> GetOrderItems(int OrderID)
        {
            // Create a new generic list of "OrderItemsList" object
            // to hold all the available Order items
            var OrderItemsList = new List<OrderItemM>();
            
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error Handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewOrderItems", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;

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
                                // Create a new "OrderItem" object
                                var Item = new OrderItemM
                                {
                                    // set the data received from db to the "Item" Object
                                    ItemID = (int)RecReader["Item_Id"],
                                    ItemPrice = (decimal)RecReader["item_Price"],
                                    ItemQuantity = (decimal)RecReader["item_Quantity"],
                                    ProductName = RecReader["Product_Name"].ToString(),
                                    CategoryName = RecReader["productCategory"].ToString(),
                                    RetailUnit = RecReader["product_RetailUnit"].ToString(),
                                };
                                    Item._QuoteList = await new OrderItemQuoteM().GetOrderItemQuoteAsync("ItemID",Item.ItemID).ConfigureAwait(false);

                                // Add the "Item" object to the "OrderItemsList"
                                OrderItemsList.Add(Item);
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

            return OrderItemsList;
        }

        /// <summary>
        /// This method is used to add the Current OrderItem instance.
        /// </summary>
        /// <param name="OrderID">The order ID of the current item</param>
        /// <returns>true if successful else false </returns>
        internal async Task<bool> AddOrderItem(int OrderID)
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
                    using (var sqlCommand = new SqlCommand("AddOrderItem", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = ProductName;
                        sqlCommand.Parameters.Add("@ItemPrice", SqlDbType.Decimal, 10).Value = ItemPrice;
                        sqlCommand.Parameters.Add("@ItemQuantity", SqlDbType.Decimal, 8).Value = ItemQuantity;


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
        /// This method is used to Update the Current OrderItem instance.
        /// </summary>
        /// <param name="OrderID">The order ID of the current item</param>
        /// <returns>true if successful else false </returns>
        internal async Task<bool> UpdateOrderItemAsync(int OrderID)
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
                    using (var sqlCommand = new SqlCommand("UpdateOrderItem", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = ProductName;
                        sqlCommand.Parameters.Add("@ItemPrice", SqlDbType.Decimal, 10).Value = ItemPrice;
                        sqlCommand.Parameters.Add("@ItemQuantity", SqlDbType.Decimal, 8).Value = ItemQuantity;


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
        /// This method is used to delete the parameter item from the database
        /// </summary>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> DeleteOrderItemAsync(int ItemId)
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
                    using (var sqlCommand = new SqlCommand("RemoveOrderItem", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemId;

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