
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    [Serializable]
    public class ProductCategoryM 
    {
        public string CategoryName { get; set; }

        public string CategoryImagePath { get; set; }

        public string CategoryTypes { get; set; }

        // This method is used to receive a list of
        // product categories from DB
        // If ProductCategory parameter is "" (null) all categories will be returned
        // otherwise only the parameter product category will be returned if it exists
        internal async Task<List<ProductCategoryM>> GetCategoriesAsync(string ProductCategory)
        {
            // Create a new generic list of "ProductCategory" object
            // to hold all the available categories
            var CategoryList = new List<ProductCategoryM>();
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
                    using (var sqlCommand = new SqlCommand("ViewProductCategories", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@CategoryName", SqlDbType.VarChar, 80).Value = ProductCategory;

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
                                var Cat = new ProductCategoryM
                                {
                                    // set the data received from db to the "Cat" Object
                                    CategoryName = RecReader["ProductCategory"].ToString().TrimEnd(),
                                    CategoryImagePath = RecReader["ProductCategory_imgPath"].ToString().TrimEnd()
                                };

                                // Add the "Cat" object to the "CategoryList"
                                CategoryList.Add(Cat);
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

            return CategoryList;
        }

        // This method is used to add the current category instant to the database
        internal async Task<bool> AddProductCategoryAsync()
        {
            var ReturnValue = false;

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("AddProductCategory", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@CategoryName", SqlDbType.VarChar, 80).Value = CategoryName;
                    sqlCommand.Parameters.Add("@CatImagePath", SqlDbType.VarChar, 500).Value = CategoryImagePath;
                    sqlCommand.Parameters.Add("@TypeName", SqlDbType.VarChar, 80).Value = CategoryTypes;

                    // Open the database connection
                    sqlConn.Open();
                    // Adding asynchronous capability.
                    // by doing so the following lines of code will
                    // become asynchronous
                    await Task.Run(() =>
                    {

                        sqlCommand.ExecuteNonQuery();
                    }).ConfigureAwait(false);
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

            return ReturnValue;
        }

        // This method is used to delete product category from the database
        internal async Task<bool> DeleteProductCategoryAsync(string categoryName, string categoryImagePath)
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
                    using (var sqlCommand = new SqlCommand("RemoveProductCategory", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@CategoryName", SqlDbType.VarChar, 80).Value = categoryName;

                        // Open the database connection
                        sqlConn.Open();

                        sqlCommand.ExecuteNonQuery();
                        ReturnValue = true;

                        File.Delete(SystemSetting.WebRootPath + categoryImagePath);
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

        // This method is used to update product category information. This method will not update the
        // product category type
        internal async Task<bool> UpdateProductCategoryAsyc(string OldCategoryName, string NewCategoryName, IFormFile CategoryImageIFormFile, string OldCategoryImagePath)
        {
            var ReturnValue = false;

            if (NewCategoryName == null || NewCategoryName.Replace(" ", "").Length < 1)
            {
                NewCategoryName = OldCategoryName;
            }

            CategoryName = NewCategoryName;
            CategoryImagePath = OldCategoryImagePath;
            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("UpdateProductCategory", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@FromCategoryName", SqlDbType.VarChar, 80).Value = OldCategoryName;
                    sqlCommand.Parameters.Add("@ToCategoryName", SqlDbType.VarChar, 80).Value = NewCategoryName;
                    sqlCommand.Parameters.Add("@CatImagePath", SqlDbType.VarChar, 500).Value = CategoryImagePath;

                    // Open the database connection
                    sqlConn.Open();

                    sqlCommand.ExecuteNonQuery();
                    ReturnValue = true;
                    // If new name update is successful then delete and new Image is not null
                    // Then delete the old image and save the new image
                    // Then update the database record again
                    if (ReturnValue && CategoryImageIFormFile != null)
                    {
                        ReturnValue = await SaveCategoryImageAsync(CategoryImageIFormFile, NewCategoryName).ConfigureAwait(false);
                        await UpdateProductCategoryAsyc(NewCategoryName, null, null, CategoryImagePath).ConfigureAwait(false);
                        File.Delete(SystemSetting.WebRootPath + OldCategoryImagePath);
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

        internal bool AddSupplierProductCategory(int Supplier_Id, string ProductCategory)
        {
            var ReturnValue = false;

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("AddSupplierProductCategory", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@SupplierId", SqlDbType.Int).Value = Supplier_Id;
                    sqlCommand.Parameters.Add("@ProductCategory", SqlDbType.VarChar, 80).Value = ProductCategory;

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

            return ReturnValue;
        }


        internal bool DeleteSupplierProductCategory(int Supplier_Id, string ProductCategory)
        {
            var ReturnValue = false;

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("RemoveSupplierProductCategory", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@SupplierId", SqlDbType.Int).Value = Supplier_Id;
                    sqlCommand.Parameters.Add("@ProductCategory", SqlDbType.VarChar, 80).Value = ProductCategory;

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

            return ReturnValue;
        }

        // This method is used to save the category image in "Images" folder of the wwwroot
        // and save the path and file name in database
        internal async Task<bool> SaveCategoryImageAsync(IFormFile CategoryImageIFormFile, string FileName)
        {
            try
            {
                var RandomNum = new Random();
                // Get A random file format
                FileName += RandomNum.Next(0, 100) + "." + CategoryImageIFormFile.FileName.Split(".")[1];

                // Full path of where the image file will be saved. (wwwroot/Images/Category/FILENAME FileFormat)
                var filePath = Path.Combine(SystemSetting.WebRootPath + @"\images\Category\", FileName);

                // Create a new file stream and set the FileMode to create
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Pass the file stream to the "CopyToAsync" method of the received parameter (IformFile)
                    await CategoryImageIFormFile.CopyToAsync(fileStream).ConfigureAwait(false);
                }
                // Set the Category image path property to the relative path of the saved image
                CategoryImagePath = @"\images\Category\" + FileName;
                // return true
                return true;
            }
            catch (Exception ex)
            {
                // For debugging purposes
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        internal async Task<List<ProductCategoryM>> GetSupplierCategoriesAsync(int supplierID)
        {
            // Create a new generic list of "ProductCategory" object
            // to hold all the available Product Categories for a supplier
            var SupplierProductCategoryList = new List<ProductCategoryM>();

            // Instantiate new SQL Connection and pass the "DbConnectionString"
            // from the "SystemSetting" class as parameter
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            try // Error Handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("ViewSupplierProductCategory", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@SupplierId", SqlDbType.Int).Value = supplierID;

                    // Open the database connection
                    sqlConn.Open();

                    // Receive all the relevant records by calling the
                    // "ExecuteReader" method of "sqlCommand" Object
                    var RecReader = await sqlCommand.ExecuteReaderAsync().ConfigureAwait(false);

                    // Check if the there are any records
                    if (RecReader.HasRows)
                    {
                        // Loop through the records
                        while (RecReader.Read())
                        {
                            // Create a new "ProductCategory" object
                            var productCategory = new ProductCategoryM
                            {
                                // set the data received from db to the "Supplier" Object
                                CategoryName = RecReader["ProductCategory"].ToString()
                            };
                            // Add the "productCategory" object to the "SupplierProductCategoryList"
                            SupplierProductCategoryList.Add(productCategory);
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
            return SupplierProductCategoryList;
        }
    }
}