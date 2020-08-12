using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    [BindProperties]
    public class SupplierRCModel : PageModel
    {
        public SupplierM _Supplier { get; set; }
        public List<ProductCategoryM> _CategoryList { get; set; }
        public List<SupplierM> _SupplierList { get; set; }

        public string _FilterCategory { get; set; }
        public string _SearchValue { get; set; }
        public int _ShowItemPerPage { get; set; } = 5;
        public int _ShowPageNum { get; set; } = 1;

        public async Task OnGetAsync(string PanelNum)
        {
            switch (PanelNum)
            {
                case "Search":
                case "3":
                case null:
                    ViewData["Search"] = "active";
                    // Populate all the product category drop-downs
                    _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
                    break;
                case "Add":
                    ViewData["Add"] = "active";
                    break;
            }
        }

        public async Task OnPostAddAsync()
        {
            if (await _Supplier.AddSupplierAsync().ConfigureAwait(false))
            {
                // Populate all the product category drop-downs
                _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
                ViewData["SuccessMsg"] = "Add Successful";
            } else
            {
                _CategoryList = null;
                ViewData["FailedMsg"] = "Add Failed";
                ViewData["Add"] = "active";
            }

        }
        public async Task OnPostEditAsync()
        {
            if (await _Supplier.UpdateSupplierAsync().ConfigureAwait(false))
            {
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
                ViewData["SuccessMsg"] = "Edit Successful";
            }
            else
            {
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
                ViewData["FailedMsg"] = "Edit Failed";
                ViewData["View"] = "active";
            }
        }
        public async Task OnPostDeleteAsync()
        {
            if (await _Supplier.DeleteSupplierAsync().ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Delete Successful";
                ViewData["SuccessRedirect"] = "href=/Suppliers/Search";
            }
            else
            {
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
                ViewData["FailedMsg"] = "Delete Failed";
                ViewData["View"] = "active";
            }
        }
        public async Task OnPostDeleteTagCategoryAsync(string Category_Name)
        {
            if (new ProductCategoryM().DeleteSupplierProductCategory(_Supplier.SupplierID, Category_Name))
            {
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
            }
            else
            {
                ViewData["FailedMsg"] = "Add Failed";
                ViewData["View"] = "active";
            }
        }
        public async Task OnPostAddTagCategoryAsync(string CategoryName)
        {
            if(new ProductCategoryM().AddSupplierProductCategory(_Supplier.SupplierID, CategoryName))
            {
                await OnPostViewAsync(_Supplier.SupplierID).ConfigureAwait(false);
            }
            else
            {
                ViewData["FailedMsg"] = "Add Failed";
                ViewData["View"] = "active";
            }
        }


        public async Task OnPostSearchSupplierAsync()
        {
            if (_SearchValue != null )
            {
                _SupplierList = await new SupplierM().GetSuppliersAsync("", "Off").ConfigureAwait(false);
            }
            else
            {
                _SupplierList = new List<SupplierM>();
            }
            Int32.TryParse(_SearchValue, out int IntSearchValue);
            foreach (var item in _SupplierList.ToArray())
            {
                if (item.SupplierID != Convert.ToInt32(IntSearchValue) && !item.Email.Contains(_SearchValue) && !item.Company.Contains(_SearchValue))
                {
                    _SupplierList.Remove(item);
                }
            }
            FilterSuppliers();
            ViewData["Search"] = "active";

            if (_SupplierList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }

            WriteSearchTempData();
            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
        }
        // This method is used to show all suppliers on the database
        public async Task OnPostShowAllSuppliersAsync()
        {

            _SupplierList = await new SupplierM().GetSuppliersAsync("", "Off").ConfigureAwait(false);

            FilterSuppliers();
            if (_SupplierList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }
            ViewData["Search"] = "active";

            _SearchValue = "ShowAll";
            WriteSearchTempData();
            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);
        }
        public async Task OnPostViewAsync(int SelectIDValue)
        {
            if (SelectIDValue > 0)
            {
                _Supplier = (await new SupplierM().GetSuppliersAsync(SelectIDValue.ToString(), "SupplierID").ConfigureAwait(false))[0];

                ViewData["View"] = "active";
            }
            else
            {
                ViewData["FailedMsg"] = "Failed to load the order.";
            }
            // Populate all the product category drop-downs
            _CategoryList = await new ProductCategoryM().GetCategoriesAsync("").ConfigureAwait(false);

        }
        public async Task OnPostPageControllerAsync(int ShowPageNum)
        {
            if (TempData["TempSearchValuesSuppliers"] != null)
            {
                var ShowItemPerPage = _ShowItemPerPage;
                ReadSearchTempData();
                _ShowPageNum = ShowPageNum;
                _ShowItemPerPage = ShowItemPerPage;

                switch (_SearchValue)
                {
                    case "ShowAll":
                        await OnPostShowAllSuppliersAsync().ConfigureAwait(false);
                        break;
                    default:
                        await OnPostSearchSupplierAsync().ConfigureAwait(false);
                        break;
                }
                // if the value of the "_ShowPageNum" is more than total pages for
                // the current Supplier list or if it is 0
                if (_ShowPageNum > Convert.ToInt32(Math.Ceiling((_SupplierList.Count + 0.0) / _ShowItemPerPage)))
                {
                    // Set the "_ShowPageNum" to the max page number allowed for the
                    // current Supplier list
                    _ShowPageNum = Convert.ToInt32(Math.Ceiling((_SupplierList.Count + 0.0) / _ShowItemPerPage));

                }
                if (_ShowPageNum == 0)
                {
                    _ShowPageNum = 1;
                }
            }
        }
        private void FilterSuppliers()
        {
            if (_FilterCategory != "All")
            {
                foreach (var item in _SupplierList.ToArray())
                {
                    var Found = false;
                    foreach (var categ in item.CategoryList)
                    {
                        if (categ.CategoryName == _FilterCategory)
                        {
                            Found = true;
                        }
                    }
                    if (!Found)
                    {
                        _SupplierList.Remove(item);
                    }
                }
            }
        }
        private void WriteSearchTempData()
        {
            // TempData used to hold search results temporarily
            TempData["TempSearchValuesSuppliers"] = _SearchValue + "@@" + _FilterCategory + "@@" + _ShowItemPerPage + "@@" + _ShowPageNum;

            if (_SearchValue == "ShowAll")
            {
                _SearchValue = "";
            }
        }
        private void ReadSearchTempData()
        {
            var TempSearchData = TempData["TempSearchValuesSuppliers"].ToString().Split("@@");
            _SearchValue = TempSearchData[0];
            _FilterCategory = TempSearchData[1];
            _ShowItemPerPage = Convert.ToInt32(TempSearchData[2]);
            _ShowPageNum = Convert.ToInt32(TempSearchData[3]);

            TempData.Remove("TempSearchValuesSuppliers");
        }
    }
}