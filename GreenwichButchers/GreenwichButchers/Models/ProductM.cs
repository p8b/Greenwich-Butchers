
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
    public class ProductM : ProductCategoryM 
    {
        public string ProductName { get; set; }

        public string NewProductName { get; set; }

        public string RetailUnit { get; set; }

        public decimal RetailPrice { get; set; }

        public decimal ItemQuantity { get; set; }

        public List<StockM> StockInfo { get; set; }

        #region ** Get Product Method

        /// <summary>
        /// This method is used to receive a list of products
        /// </summary>
        /// <param name="Value">Used to pass Product Name and Category</param>
        /// <param name="FilterBy">ProductName, Category, Off(View All)</param>
        /// <returns>List of Product class</returns>
        internal async Task<List<ProductM>> GetProductsAsync(string Value, string FilterBy)
        {
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);





            // Create a generic List of "Product" Class
            // to be used as return value of this method
            var ProductList = new List<ProductM>();

            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("ViewProducts", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@Value", SqlDbType.VarChar, 300).Value = Value;
                    sqlCommand.Parameters.Add("@FilterBy", SqlDbType.VarChar, 30).Value = FilterBy;

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
                            var prod = new ProductM
                            {
                                // Set the product information received to the current
                                // instance of "Product" class
                                ProductName = RecReader["Product_Name"].ToString().TrimEnd(),
                                NewProductName = RecReader["Product_Name"].ToString().TrimEnd(),
                                RetailUnit = RecReader["product_RetailUnit"].ToString().TrimEnd(),
                                RetailPrice = (decimal)RecReader["product_RetailPrice"],
                                CategoryName = RecReader["productCategory"].ToString().TrimEnd()
                            };

                            prod.StockInfo = await new StockM().GetStockInfoAsync("Product", prod.ProductName).ConfigureAwait(false);
                            // Add the current object to the list
                            ProductList.Add(prod);
                        }
                    }
                }
                // Pass the Product list to "AddNewStockAutoAsync" method in order to check
                // if all the products have stock records.
                // Return the List which is returned from "AddNewStockAutoAsync" method
                return await AddNewStockAutoAsync(ProductList).ConfigureAwait(false);
            }
            // Exception error handler
            catch (Exception e)
            {
                // For debugging purposes
                Debug.WriteLine(e.Message);

                // return empty list
                return ProductList;
            }
            finally
            {
                // Make sure the db connection is closed
                sqlConn.Close();
            }
        }

        #endregion

        #region ** Delete Product Method

        /// <summary>
        /// This method is used to delete a product from the database.
        /// Note that deleting the product record, will effect related stock,
        /// order item and Order Quotes records
        /// </summary>
        /// <param name="deleteThisProduct">The Name of the product</param>
        internal async Task DeleteProductAsync(string deleteThisProduct)
        {
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
                    using (var sqlCommand = new SqlCommand("RemoveProduct", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = deleteThisProduct;

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

        #endregion

        #region ** Add Product Method

        /// <summary>
        /// This method is used to add product to the database
        /// </summary>
        /// <param name="ThisProduct">Product object</param>
        /// <returns></returns>
        internal async Task<bool> AddProductAsync(ProductM ThisProduct)
        {
            // Return value
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
                    using (var sqlCommand = new SqlCommand("AddProduct", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = ThisProduct.ProductName;
                        sqlCommand.Parameters.Add("@RetailUnit", SqlDbType.VarChar, 50).Value = ThisProduct.RetailUnit;
                        sqlCommand.Parameters.Add("@RetailPrice", SqlDbType.Decimal, 6).Value = ThisProduct.RetailPrice;
                        sqlCommand.Parameters.Add("@Category", SqlDbType.VarChar, 80).Value = ThisProduct.CategoryName;

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

        #endregion

        #region ** Update a list of Product including stock information Method

        /// <summary>
        /// This Method is used to update a list of product containing one or more products.
        /// </summary>
        /// <param name="ProductList">Product List to be updated</param>
        internal async Task<ListError> UpdateProductListAsync(List<ProductM> ProductList)
        {
            // This object is used to return list of errors
            // the default value of the status property is set to true
            var errorList = new ListError
            {
                Status = true
            };

            //check if the product list is null or empty
            if (ProductList != null && ProductList.Count != 0)
            {
                var ItemNum = 0;
                // Loop through the product list
                foreach (var item in ProductList)
                {
                    // update the product information and if the return value is false
                    if (!await UpdateProductAsync(item).ConfigureAwait(false))
                    {
                        if (item.NewProductName != item.ProductName)
                        {
                            var customError = new CustomError
                            {
                                ErrNumber = ItemNum,
                                ItemErrorMsg = "** Product Name \"" + item.ProductName + "\" failed to update to \"" + item.NewProductName + "\". Product name already exists."
                            };

                            // set the update status to false
                            errorList.Status = false;
                            // add the error massage to the list
                            errorList.ErrorList.Add(customError);
                        }
                    }

                    // Loop through the stock info list of current product
                    foreach (var itemStock in item.StockInfo)
                    {
                        // Update each to the stock object to the database
                        await new StockM().AddUpdateStockInfoAsync(itemStock.ProductName,itemStock.StockQuantity, itemStock.LocationName).ConfigureAwait(false);
                    }
                    ItemNum++;
                }
            }
            return errorList;
        }

        #endregion 

        #region ** Update One Product Method

        /// <summary>
        /// This local method is used to update one product in the database.
        /// Used in "UpdateProductListAsync" method.
        /// </summary>
        /// <param name="Prod">Object of Product </param>
        private async Task<bool> UpdateProductAsync(ProductM Prod)
        {
            // Return value
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
                    using (var sqlCommand = new SqlCommand("UpdateProduct", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ProductName", SqlDbType.VarChar, 300).Value = Prod.ProductName;
                        sqlCommand.Parameters.Add("@NewProductName", SqlDbType.VarChar, 300).Value = Prod.NewProductName;
                        sqlCommand.Parameters.Add("@RetailUnit", SqlDbType.VarChar, 50).Value = Prod.RetailUnit;
                        sqlCommand.Parameters.Add("@RetailPrice", SqlDbType.Decimal, 6).Value = Prod.RetailPrice;
                        sqlCommand.Parameters.Add("@productCategory", SqlDbType.VarChar, 80).Value = Prod.CategoryName;

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

        #endregion

        #region ** Add a list of stock information method

        /// <summary>
        /// This method is used to create default stock information for a product that
        /// does not have set record.
        /// This method will also ensure that new stock locations are added to all the products
        /// The quantity value will be set to "0"
        /// </summary>
        /// <param name="ProductList">List of Product class to be able to compare locations</param>
        /// <returns>Returns the update product list</returns>
        private async Task<List<ProductM>> AddNewStockAutoAsync(List<ProductM> ProductList)
        {
            // Receive all the stock location records
            var Locations = await new StockLocationM().GetStockLocationsAsync().ConfigureAwait(false);

            // loop through the product list
            foreach (var product in ProductList)
            {
                // If the number of records within the "StockInfo" property
                // of the current product which holds the information about the
                // product's stock information is NOT equal to the number of
                // stock location records
                if (product.StockInfo.Count != Locations.Count)
                {
                    // Loop through the location records
                    foreach (var location in Locations)
                    {
                        // this method is used to check if the current stock location
                        // is already linked to the current product.
                        // Set to false by default in order to add unlinked stock location and product.
                        var HasStock = false;

                        // Loop through the current product stock information
                        foreach (var stock in product.StockInfo)
                        {
                            // If the current Stock location record is equal to current product's location name
                            if (location.LocationName == stock.LocationName)
                            {
                                // Then the link already exists
                                HasStock = true;
                            }
                        }
                        // If the value of "HasStock" is false which means there are no links between the current
                        // current stock location and current product
                        if (!HasStock)
                        {
                            // If so, Create a new instance of stock class
                            var NewStock = new StockM
                            {
                                // With the following properties
                                // Set the product name to the current product
                                ProductName = product.ProductName,
                                // Set the quantity to 0
                                StockQuantity = 0,
                                // Set the stock location name to the current stock location
                                LocationName = location.LocationName
                            };

                            // Call the "AddProductStockLink" to add the link to the database
                            await new StockM().AddUpdateStockInfoAsync(product.ProductName, 0, location.LocationName).ConfigureAwait(false);

                            // Add the new stock information to the current product of product list
                            product.StockInfo.Add(NewStock);
                        }
                    }
                }
            }
            // Return the modified product list
            return ProductList;
        }

        #endregion
    }
}