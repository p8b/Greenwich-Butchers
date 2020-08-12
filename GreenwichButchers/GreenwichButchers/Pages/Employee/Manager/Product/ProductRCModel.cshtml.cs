using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    // Bind all the properties with the view
    [BindProperties]
    public class ProductRCModel : PageModel
    {
        // Single Object properties to set and get view elements
        public ProductCategoryM _Category { get; set; }

        public ProductM _Product { get; set; }
        public StockLocationM _StockLocation { get; set; }

        //List of Object properties to set and get view elements
        public List<ProductM> _ProductList { get; set; }

        public List<ProductCategoryM> _CategoryList { get; set; }
        public List<StockLocationM> _StockLocationList { get; set; }
        public List<CustomError> _ErrorList { get; set; }

        // string properties to set and get Search values
        public string _CategoryName { get; set; }
        public string _SearchValue { get; set; }
        // property used to control the pages
        public int _ShowPageNum { get; set; } = 1;
        // Property used to control the items per page for
        // the search result of "Product/Stock (Modify)" Page
        public int _ShowItemPerPage { get; set; } = 5;

        // OnGet Method which is used for every get request
        public async Task OnGet(string Panel)
        {
            #region Control The Panels

            // If the selected panel is Product/Stock (Modify),
            // Product (Add) or Product Category (Add,Modify)
            if (Panel == "AndStockEdit" || Panel == "Add" || Panel == "Category")
            {
                // Populate all the product category drop-downs
                _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
            }

            // if the selected panel is Stock Location (Add, Modify)
            if (Panel == "StockLocation")
            {
                // Populate the stock location drop-down menu
                _StockLocationList = await new StockLocationM().GetStockLocationsAsync().ConfigureAwait(false);
            }

            // If Panel number is null
            if (Panel == null)
            {
                // Redirect the user to Product/Stock (Modify) Page
                Response.Redirect("/Product/AndStockEdit", false);
            }
            else // panel number is present
            {
                // Use the panel number to set that panel active in View
                ViewData[Panel] = "active show";
            }

            #endregion Control The Panels

            // This is used to set the visibility of "No Records Found" label
            ViewData["NoRecordsFound"] = false;

            if (TempData["NewProductName"] != null || TempData["DDCategoryName"] != null)
            {
                if (TempData["NewProductName"] != null)
                {
                    // Search the value of TempData which is the name of the
                    // recently added product
                    _SearchValue = TempData["NewProductName"].ToString();
                    TempData.Remove("NewProductName");
                }

                if (TempData["DDCategoryName"] != null)
                {
                    // Get the product category from TempData
                    _CategoryName = TempData["DDCategoryName"].ToString();
                    TempData.Remove("DDCategoryName");
                }

                // With the Search value and category set call Search Product method
                await OnPostSearchProduct("AndStockEdit").ConfigureAwait(false);
            }
        }

        #region *** Panel AndStockEdit "Product/Stock (Modify)"

        // This method is used to filter the product list by category
        public async Task OnPostSearchProduct(string Panel)
        {
            //  Set the category drop-down menu values
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);

            // Set FilterBy value to category
            var FilterBy = "Category";

            // If the value of the drop-down menu is "All" or empty
            if (_CategoryName == "All" || _CategoryName == null || _CategoryName?.Length == 0)
            {
                // Set the FilterBy value to "Off" in order to
                // receive all the products
                FilterBy = "Off";
            }

            // Calling the "GetProductsAsync" method of product class
            // and pass the "_CategoryName" and "FilterBy" value as parameters
            // If filter by is set to "Off" the value of "_CategoryName" is not used
            _ProductList = await new ProductM().GetProductsAsync(_CategoryName, FilterBy).ConfigureAwait(false);

            // Check if the search value is set. The search value must not be null, empty or only spaces
            if (!string.IsNullOrEmpty(_SearchValue) && _SearchValue.Replace(" ", "").Length != 0 && _ProductList != null)
            {
                try // For Error handling
                {
                    // Turn the search value to all upper case characters
                    // this enables us to search without any case sensitivity
                    _SearchValue = _SearchValue.ToLower();
                    // Loop through the Product List
                    foreach (var item in _ProductList.ToArray())
                    {
                        // Make the ProductName to all upper case and then check if
                        // it contains the search result
                        if (!item.ProductName.ToLower().Contains(_SearchValue))
                        {
                            // if the product name does not have the search result
                            // Remove the item from the product list
                            _ProductList.Remove(item);
                        }
                    }
                }
                // Catch the "InvalidOperationException"
                // This because when the last item from the "_ProductList" is
                // removed the "InvalidOperationException" is thrown since the list
                // will be come null.
                catch (InvalidOperationException ex)
                {
                    // Use for debugging
                    Debug.WriteLine(ex);
                }
            }
            // Set the appropriate panel to active and show
            // The following ViewData is used to set the attribute
            // of selected Panel number to "active show"
            ViewData[Panel] = "active show";

            // This is used to set the visibility of "No Records Found" label
            ViewData["NoRecordsFound"] = true;

            // TempData is used to hold the state of the page.
            TempData["_CategoryName"] = _CategoryName;
            TempData["_SearchValue"] = _SearchValue;
            TempData["_ShowPageNum"] = _ShowPageNum;
            TempData["_ShowItemPerPage"] = _ShowItemPerPage;
        }

        // This method is used to update product information
        public async Task OnPostUpdateProduct()
        {
            // Set the correct panel visible
            ViewData["AndStockEdit"] = "active show";

            var confirmation = await _Product.UpdateProductListAsync(_ProductList).ConfigureAwait(false);
            if (!confirmation.Status)
            {
                foreach (var item in confirmation.ErrorList)
                {
                    _ErrorList.Add(item);
                }
            }
            else
            {
                ViewData["SuccessMsg"] = "Update Successful";
            }
            ReadPageSate();
            // Repopulate the search values and results
            await OnPostSearchProduct("AndStockEdit").ConfigureAwait(false);
        }

        // This method is used to delete a product which is received as parameter
        public async Task OnPostDelete(string Product, int currentPageNum)
        {
            // Pass the product name to the "DeleteProduct" method of product category
            await new ProductM().DeleteProductAsync(Product).ConfigureAwait(false);

            await OnPostPageControllerAsync(currentPageNum).ConfigureAwait(false);

            ViewData["AndStockEdit"] = "active show";
        }

        // This method is used to control the pagination for the search result
        public async Task OnPostPageControllerAsync(int ShowPageNum)
        {
            TempData["_ShowItemPerPage"] = _ShowItemPerPage;
            TempData["_ShowPageNum"] = ShowPageNum;

            ReadPageSate();

            //Set the ShowPageNum property with the value received as current method parameter
            //_ShowPageNum = ShowPageNum;

            // Call for search product method to retrieve the new informations
            await OnPostSearchProduct("AndStockEdit").ConfigureAwait(false);

            // if the value of the "_ShowPageNum" is more than total pages for
            // the current customer list or if it is 0
            if (_ShowPageNum > Convert.ToInt32(Math.Ceiling((_ProductList.Count + 0.0) / _ShowItemPerPage)))
            {
                // Set the "_ShowPageNum" to the max page number allowed for the
                // current customer list
                _ShowPageNum = Convert.ToInt32(Math.Ceiling((_ProductList.Count + 0.0) / _ShowItemPerPage));
            }

            // If no page number is specified the go to page AndStockEdit (Product/Stock (Modify))
            if (_ShowPageNum == 0)
                _ShowPageNum = 1;
        }

        // This method is used to read and delete "TempSearchValues" cookie
        private void ReadPageSate()
        {
            if (TempData["_ShowPageNum"] != null)
            {
                _ShowPageNum = Convert.ToInt32(TempData["_ShowPageNum"]);
            }
            if (TempData["_ShowItemPerPage"] != null)
            {
                _ShowItemPerPage = Convert.ToInt32(TempData["_ShowItemPerPage"]);
            }
            if ( TempData["_CategoryName"] != null )
            {
                _CategoryName = TempData["_CategoryName"].ToString();
            }
            if (TempData["_SearchValue"] != null)
            {
                _SearchValue = TempData["_SearchValue"].ToString();
            }
            TempData.Keep("_ShowPageNum");
            TempData.Keep("_ShowItemPerPage");
            TempData.Keep("_CategoryName");
            TempData.Keep("_SearchValue");
        }

        #endregion *** Panel AndStockEdit "Product/Stock (Modify)"

        #region *** Panel Add "Product (Add)"

        // This method is used to add new products
        public async Task OnPostAddProduct()
        {
            // Set the appropriate panel to active and show
            // The following view data is used to set the attribute
            // of "Product/Stock (Modify)" panel
            ViewData["Add"] = "active show";

            if (ModelState.IsValid)
            {
                if (!await _Product.AddProductAsync(_Product).ConfigureAwait(false))
                {
                    ViewData["FailedMsg"] = "Add Failed. Product name already exists";
                }
                else
                {
                    ViewData["SuccessMsg"] = "Add Successful";
                }
            }

            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
        }

        //This method allows the user to add stock information
        //for newly created product form "Product (Add)" panel
        public void OnPostAddStockValues(string NewProductName, string DDCategoryName)
        {
            TempData["NewProductName"] = NewProductName;
            TempData["DDCategoryName"] = DDCategoryName;

            Response.Redirect("/Employee/Product/AndStockEdit?handler=SearchProduct", false);
        }

        #endregion *** Panel Add "Product (Add)"

        #region *** Panel Category "Product Category (Add/Modify)"

        // This method is used to add new category
        public async Task OnPostAddCategory(IFormFile ImgFile)
        {
            // Save the image to (wwwroot/Images/Category)
            var SaveImageStatus = await _Category.SaveCategoryImageAsync(ImgFile, _Category.CategoryName).ConfigureAwait(false);
            // Add the category record to the database
            var AddCategoryStatus = await _Category.AddProductCategoryAsync().ConfigureAwait(false);

            // Set the active panel to "Product Category (Add/Modify)"
            ViewData["Category"] = "active show";

            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);

            // If both methods return true
            if (SaveImageStatus && AddCategoryStatus)
            {
                ViewData["SuccessMsg"] = "Successful";
                return;
            }

            // If Save image return false
            if (!SaveImageStatus)
            {
                ViewData["FailedMsg"] = "Failed to save the Image.";
                return;
            }

            // If only adding to the database fails
            if (!AddCategoryStatus)
            {
                ViewData["FailedMsg"] = "Category Name already exists.";
            }
        }

        // This method is used to select a category to modify
        public async Task OnPostEnableCategoryModify()
        {
            ViewData["Category"] = "active show";
            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
            if (_Category.CategoryName != null)
            {
                ViewData["SelectedCategoryName"] = _Category.CategoryName;
                foreach (var item in _CategoryList)
                {
                    if (item.CategoryName == _Category.CategoryName)
                    {
                        ViewData["SelectedCategoryImagePath"] = item.CategoryImagePath;
                    }
                }
                _Category.CategoryName = "";
            }
        }

        // This method is used to update existing categories
        public async Task OnPostUpdateCategory(IFormFile ImgFileModify, string SelectedCategoryName, string SelectedCategoryImagePath)
        {
            ViewData["Category"] = "active show";
            if (_CategoryName != null || ImgFileModify != null)
            {
                if (await _Category.UpdateProductCategoryAsyc(
                   SelectedCategoryName,
                   _CategoryName,
                   ImgFileModify,
                   SelectedCategoryImagePath
               ).ConfigureAwait(false))
                {
                    ViewData["SuccessMsg"] = "Update Successful";
                    // Populate all the product category drop-downs
                    _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
                }
                else
                {
                    ViewData["FailedMsg"] = "Update Failed. Product Name already exists.";
                    _Category.CategoryName = SelectedCategoryName;
                }
            }

            await OnPostEnableCategoryModify().ConfigureAwait(false);
        }

        // This method is used to delete a category
        public async Task OnPostDeleteCategoryAsync(string SelectedCategoryName, string SelectedCategoryImagePath)
        {
            ViewData["Category"] = "active show";

            if (await _Category.DeleteProductCategoryAsync(SelectedCategoryName, SelectedCategoryImagePath).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Delete Successful";
            }
            else
            {
                ViewData["FailedMsg"] = "Delete Failed";
            }

            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
        }

        #endregion *** Panel Category "Product Category (Add/Modify)"

        #region *** Panel StockLocation "Stock Location (Add/Modify)"

        public async Task OnPostAddStockLocation()
        {
            ViewData["StockLocation"] = "active show";
            if (_StockLocation.LocationName != null && _StockLocation.LocationName.Replace(" ", "").Length > 0)
            {
                if (await _StockLocation.AddStockLocationAsync().ConfigureAwait(false))
                {
                    ViewData["SuccessMsg"] = "Add Successful";
                }
                else
                {
                    ViewData["FailedMsg"] = "Add Failed. Location already exists.";
                }
            }
            _StockLocationList = await _StockLocation.GetStockLocationsAsync().ConfigureAwait(false);
        }

        public async Task OnPostUpdateStockLocation(string NewStockLocation)
        {
            ViewData["StockLocation"] = "active show";
            if (_StockLocation.LocationName?.Replace(" ", "").Length > 0)
            {
                if (await _StockLocation.UpdateStockLocationAsync(NewStockLocation).ConfigureAwait(false))
                {
                    ViewData["SuccessMsg"] = "Update Successful";
                }
                else
                {
                    ViewData["FailedMsg"] = "Update Failed. Location already exists.";
                }
            }
            _StockLocationList = await _StockLocation.GetStockLocationsAsync().ConfigureAwait(false);
        }

        public async Task OnPostDeleteStockLocation()
        {
            ViewData["StockLocation"] = "active show";
            if (_StockLocation.LocationName != null && _StockLocation.LocationName.Replace(" ", "").Length > 0)
            {
                if (await _StockLocation.DeleteStockLocationAsync().ConfigureAwait(false))
                {
                    ViewData["SuccessMsg"] = "Delete Successful";
                }
                else
                {
                    ViewData["FailedMsg"] = "Delete Failed";
                }
            }
            _StockLocationList = await _StockLocation.GetStockLocationsAsync().ConfigureAwait(false);
        }

        #endregion *** Panel StockLocation "Stock Location (Add/Modify)"
    }
}