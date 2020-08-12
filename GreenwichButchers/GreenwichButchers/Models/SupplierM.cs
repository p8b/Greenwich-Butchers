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
    public class SupplierM
    {
        public int SupplierID { get; set; }
        public string Company { get; set; }
        public string FullName { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public List<ProductCategoryM> CategoryList { get; private set;}
        public List<OrderItemQuoteM> OrderQuotes { get; set; }
                
        /// <summary>
        ///  This method is used to get the list of supplier objects with the
        ///  supplier information including their product categories.
        ///  it can also be used to get information for one supplier only.
        /// </summary>
        /// <param name="FilterValue">Only Supplier ID</param>
        /// <param name="FilterBy">"Supplier" Or "Off" to view all</param>
        /// <returns>List of supplier objects</returns>
        public async Task<List<SupplierM>> GetSuppliersAsync(string FilterValue, string FilterBy)
        {
            // Create a new generic list of "Supplier" object
            // to hold all the available Suppliers
            var SupplierList = new List<SupplierM>();

                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
                 
                try // Error Handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("ViewSupplier", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@FilterValue", SqlDbType.VarChar, 80).Value = FilterValue;
                        sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 20).Value = FilterBy;

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
                                // Create a new "ProductCategory" object
                                var Supplier = new SupplierM
                                {
                                    // set the data received from db to the "Supplier" Object
                                    SupplierID = (int)RecReader["Supplier_Id"],
                                    Company = RecReader["supplier_Company"].ToString(),
                                    FullName = RecReader["supplier_FullName"].ToString(),
                                    Tel = RecReader["supplier_Tel"].ToString(),
                                    Email = RecReader["supplier_Email"].ToString(),
                                    Description = RecReader["supplier_Description"].ToString()
                                };
                                // Add the categories for the current supplier
                                Supplier.CategoryList = await new ProductCategoryM().GetSupplierCategoriesAsync(Supplier.SupplierID);

                                // Add the "Supplier" object to the "SupplierList"
                                SupplierList.Add(Supplier);
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
            return SupplierList;
        }

        /// <summary>
        /// Add the current instance of the supplier to the database and get the
        /// new Supplier id and set it to the "SupplierID" property.
        /// </summary>
        /// <returns>return true if successful else false </returns>
        internal async Task<bool> AddSupplierAsync()
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
                    using (var sqlCommand = new SqlCommand("AddSupplier", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@Company", SqlDbType.VarChar, 200).Value = Company;
                        sqlCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 250).Value = FullName;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = Email ?? "";
                        sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, 1000).Value = Description ?? "";

                        // Open the database connection
                        sqlConn.Open();

                        // Receive all the relevant records by calling the
                        // "ExecuteReader" method of "sqlCommand" Object
                        var RecReader = sqlCommand.ExecuteReader();

                        // Check if the there are any records
                        if (RecReader.HasRows)
                        {
                            // Read the new supplier id
                            RecReader.Read();

                            SupplierID = Convert.ToInt32(RecReader["Supplier_Id"]);

                            ReturnValue = true;
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

            return ReturnValue;
        }

        /// <summary>
        /// Update the current instance of the supplier to the database
        /// </summary>
        /// <returns>return true if successful else false </returns>
        internal async Task<bool> UpdateSupplierAsync()
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
                    using (var sqlCommand = new SqlCommand("UpdateSupplier", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;
                        sqlCommand.Parameters.Add("@Company", SqlDbType.VarChar, 200).Value = Company;
                        sqlCommand.Parameters.Add("@FullName", SqlDbType.VarChar, 250).Value = FullName;
                        sqlCommand.Parameters.Add("@Tel", SqlDbType.VarChar, 15).Value = Tel;
                        sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = Email;
                        sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, 1000).Value = Description;

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
        /// Delete the current instance of the supplier from the database
        /// </summary>
        /// <returns>return true if successful else false </returns>
        internal async Task<bool> DeleteSupplierAsync()
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
                    using (var sqlCommand = new SqlCommand("RemoveSupplier", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int).Value = SupplierID;

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