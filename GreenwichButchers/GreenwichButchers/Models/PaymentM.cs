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
    public class PaymentM
    {
        public int FKOrderID { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SupplierTotalPayment { get; set; }
        public decimal TotalProfit { get; set; }
        public int ProfitMargin { get; set; }
        public string PaymentDate { get; set; }

        internal async Task<List<PaymentM>> GetAsync(string FilterBy, int OrderId)
        {
            // Create a new generic list of "PaymentList" object
            // to hold all the available Order Payment
            var PaymentList = new List<PaymentM>();


            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            await Task.Run(() =>
            {
                try // Error Handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewPayment", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 10).Value = FilterBy;
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderId;

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
                                // Create a new "Pay" object
                                var Pay = new PaymentM
                                {
                                    // set the data received from db to the "Pay" Object
                                    TotalPrice = (decimal)RecReader["payment_Total"],
                                    SupplierTotalPayment = (decimal)RecReader["payment_Supplier"],
                                    TotalProfit = (decimal)RecReader["payment_Profit"],
                                    ProfitMargin = (int)RecReader["payment_ProfitMargin"],
                                    PaymentDate = RecReader["payment_Date"].ToString()
                                };
                                if (RecReader["Order_Id"] != DBNull.Value)
                                {
                                    Pay.FKOrderID = (int)RecReader["Order_Id"];
                                }
                                // Add the "Pay" object to the "PaymentList"
                                PaymentList.Add(Pay);
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
            return PaymentList;

        }

        internal async Task<bool> AddUpdateAsync()
        {
            var ReturnValue = false;
            CalculatePayments();

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            await Task.Run(() =>
            {
                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("AddUpdatePayment", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@paymentTotal", SqlDbType.Decimal, 11).Value = TotalPrice;
                        sqlCommand.Parameters.Add("@paymentSupplier", SqlDbType.Decimal, 11).Value = SupplierTotalPayment;
                        sqlCommand.Parameters.Add("@paymentProfit", SqlDbType.Decimal, 11).Value = TotalProfit;
                        sqlCommand.Parameters.Add("@paymentProfitMargin", SqlDbType.Int).Value = ProfitMargin;
                        sqlCommand.Parameters.Add("@paymentDate", SqlDbType.VarChar, 10).Value = PaymentDate;
                        sqlCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = FKOrderID;

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
        internal async Task<bool> DeleteAsync()
        {
            var ReturnValue = false;

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            await Task.Run(() =>
            {
                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("RemovePayment", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@OrderID", SqlDbType.Int).Value = FKOrderID;


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
        private void CalculatePayments()
        {
            try
            {
                TotalProfit = (TotalPrice * ProfitMargin) / 100;
                SupplierTotalPayment = TotalPrice - TotalProfit;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}