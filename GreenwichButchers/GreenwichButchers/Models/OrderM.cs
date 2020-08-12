using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    [Serializable]
    public class OrderM
    {
        public int OrderID { get; set; }
        public int DeliveryAddressID { get; set; }
        public string Date { get; set; }
        public string DeliveryDate { get; set; }
        public string Note { get; set; }
        public string Type { get; set; } = "Bulk";
        public string Status { get; set; }
        public List<EmployeeM> EmployeeList { get; set; }
        public List<OrderItemM> ItemList { get; set; }
        public PaymentM _Payment { get; set; }
        public CustomerM _Customer { get; set; }

        /// <summary>
        /// This method is used to get One or more orders.
        /// </summary>
        /// <param name="FilterBy">CustomerID, OrderID, Status, Type or Off(View all orders)</param>
        /// <param name="FilterValue">CustomerID, OrderID, Status (Received, Pending, Completed)</param>
        /// <returns></returns>
        internal async Task<List<OrderM>> GetOrdersAsync(string FilterBy, string FilterValue)
        {
            // Create a new generic list of "Order" object
            // to hold all the available Orders
            var OrderList = new List<OrderM>();

            // Run the following code asynchronously
            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(async () =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error Handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewOrder", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@FilterValue", SqlDbType.VarChar, 300).Value = FilterValue;
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 80).Value = FilterBy;

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
                                // Create a new "Order" object
                                var order = new OrderM
                                {
                                    // set the data received from db to the "order" Object
                                    OrderID = (int)RecReader["Order_Id"],
                                    Date = RecReader["order_Date"].ToString().TrimEnd(),
                                    DeliveryDate = RecReader["order_DeliveryDate"].ToString().TrimEnd(),
                                    Note = RecReader["order_Note"].ToString().TrimEnd(),
                                    Status = RecReader["order_Status"].ToString().TrimEnd(),
                                    Type = RecReader["order_Type"].ToString().TrimEnd()
                                };
                                if (RecReader["Address_Id"] != DBNull.Value)
                                {
                                    order.DeliveryAddressID = (int)RecReader["Address_Id"];
                                }
                                order._Customer = await new CustomerM().GetOneCustomerAsync((int)RecReader["Customer_Id"]).ConfigureAwait(false);

                                order.ItemList = await new OrderItemM().GetOrderItems(order.OrderID).ConfigureAwait(false);

                                // Add the "order" object to the "OrderList"
                                OrderList.Add(order);
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

            return OrderList.OrderByDescending(i => i.OrderID).ToList();
        }
        internal async Task<bool> AddOrderAsync()
        {
            var ReturnValue = true;

            // If order Id is set
            if (OrderID > 0)
            {
                // return the value from "UpdateOrderAsync" method
                return await UpdateOrderAsync().ConfigureAwait(false);
            }


            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("AddOrder", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@OrderDate", SqlDbType.VarChar, 10).Value = DateTime.Now.ToShortDateString();
                    sqlCommand.Parameters.Add("@OrderDeliveryDate", SqlDbType.VarChar, 10).Value = DeliveryDate;
                    sqlCommand.Parameters.Add("@Note", SqlDbType.VarChar, 500).Value = Note ?? "";
                    sqlCommand.Parameters.Add("@Status", SqlDbType.VarChar, 30).Value = Status;
                    sqlCommand.Parameters.Add("@OrderType", SqlDbType.VarChar, 50).Value = Type;
                    sqlCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = _Customer.CustomerID;
                    sqlCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = DeliveryAddressID;


                    // Open the database connection
                    sqlConn.Open();

                    var RecReader = sqlCommand.ExecuteReader();

                    // Check if the there are any records
                    if (RecReader.HasRows)
                    {
                        // Loop through the records
                        while (RecReader.Read())
                        {

                            OrderID = (int)RecReader["Order_Id"];
                        }
                    }

                    if (OrderID != 0)
                    {
                        foreach (var item in ItemList)
                        {
                            if (!await item.AddOrderItem(OrderID).ConfigureAwait(false))
                            {
                                ReturnValue = false;
                            }
                        }
                    }
                    else
                    {
                        ReturnValue = false;
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

            if (!ReturnValue)
            {
                //// REMOVE THE ORDER
            }

            return ReturnValue;
        }
        internal async Task<bool> DeleteOrderAsync()
        {
            var ReturnValue = false;

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
                    using (var sqlCommand = new SqlCommand("RemoveOrder", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;


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
        internal async Task<bool> UpdateOrderAsync()
        {
            var ReturnValue = true;

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("UpdateOrder", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
                    sqlCommand.Parameters.Add("@OrderDate", SqlDbType.VarChar, 10).Value = Date;
                    sqlCommand.Parameters.Add("@OrderDeliveryDate", SqlDbType.VarChar, 10).Value = DeliveryDate;
                    sqlCommand.Parameters.Add("@Note", SqlDbType.VarChar, 500).Value = Note;
                    sqlCommand.Parameters.Add("@Status", SqlDbType.VarChar, 30).Value = Status;
                    sqlCommand.Parameters.Add("@OrderType", SqlDbType.VarChar, 50).Value = Type;
                    sqlCommand.Parameters.Add("@AddressID", SqlDbType.Int).Value = DeliveryAddressID;


                    // Open the database connection
                    sqlConn.Open();

                    sqlCommand.ExecuteNonQuery();
                    ReturnValue = true;
                    if (OrderID != 0)
                    {
                        foreach (var item in ItemList)
                        {
                            ReturnValue = false;
                            if (item.ItemID > 0 && await item.UpdateOrderItemAsync(OrderID).ConfigureAwait(false))
                            {
                                ReturnValue = true;
                            }
                            else if (item.ItemID == 0 && await item.AddOrderItem(OrderID).ConfigureAwait(false))
                            {
                                ReturnValue = true;
                            }
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

            return ReturnValue;
        }

        internal async Task GetLinkedEmployeesAsync()
        {
            // If order Id is set
            if (OrderID > 0)
            {
                EmployeeList = new List<EmployeeM>();
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
                        using (var sqlCommand = new SqlCommand("ViewEmployeeOrderLink", sqlConn)
                        {
                            // Specify the command type as stored procedure
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            // Passing the parameter values for the stored procedure
                            sqlCommand.Parameters.Add("@FilterValue", SqlDbType.Int).Value = OrderID;
                            sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 10).Value = "OrderID";

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
                                    // Create a new "EmployeeM" object
                                    var emp = new EmployeeM
                                    {
                                        EmployeeID = (int)RecReader["Employee_Id"],
                                        InitialEmp = (bool)RecReader["Initial_Employee"],
                                        Title = RecReader["person_Title"].ToString(),
                                        Name = RecReader["person_Name"].ToString(),
                                        Surname = RecReader["person_Surname"].ToString(),
                                    };
                                    // Add the "emp" object to the "EmployeeList"
                                    EmployeeList.Add(emp);
                                }
                            } else
                            {
                                EmployeeList = null;
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
            }
        }

        internal async Task LinkEmployeeAsync(int EmpID)
        {

            // If order Id is set
            if (OrderID > 0)
            {

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
                    using (var sqlCommand = new SqlCommand("AddEmployeeOrderLink", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmpID;
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;


                        // Open the database connection
                        sqlConn.Open();

                        sqlCommand.ExecuteNonQuery();
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
            }
        }
    }
}