using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    [BindProperties]
    public class EmployeesRCModel : PageModel
    {
        public EmployeeM _Employee { get; set; }
        public List<EmployeeM> _EmployeeList { get; set; }
        public string _SearchValue { get; set; }
        public int _ShowItemPerPage { get; set; } = 5;
        public int _ShowPageNum { get; set; } = 1;

        public void OnGet()
        {

            TempData.Remove("EmpShowEmployee");
            TempData.Remove("EmpSearchState");
            _SearchValue = null;
        }

        public async Task OnGetFromModifyAsync()
        {
            if (TempData["EmpSearchState"] != null)
            {
                ReadSearchData();

                if (_SearchValue == "@@ShowAll")
                {
                    _EmployeeList = await new EmployeeM().GetEmployeeAsync(0, true).ConfigureAwait(false);
                    if (_EmployeeList.Count == 0)
                    {
                        ViewData["NoRecordsFound"] = true;
                    }
                }
                else if (_SearchValue != null)
                {
                    _EmployeeList = await new EmployeeM().SearchEmployeeAsync(_SearchValue).ConfigureAwait(false);
                    if (_EmployeeList.Count == 0)
                    {
                        ViewData["NoRecordsFound"] = true;
                    }
                }
                TempData["EmpSearchState"] = _SearchValue + "@@@" + _ShowPageNum + "@@@" + _ShowItemPerPage;
                _SearchValue = null;
            }

        }
        // This method is used to Search for Employees by
        // Name, Surname, Email or Employee ID
        public async Task OnPostSearchEmployeeAsync()
        {
            if (_SearchValue == "@@ShowAll" || _SearchValue == null)
            {
                return;
            }

            _EmployeeList = await new EmployeeM().SearchEmployeeAsync(_SearchValue.Replace(" ", "")).ConfigureAwait(false);
            if (_EmployeeList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }

            // TempData used to hold search results temporarily
            TempData["EmpSearchState"] = _SearchValue + "@@@" + _ShowPageNum + "@@@" + _ShowItemPerPage;


        }

        public async Task OnPostShowAllEmployeeAsync()
        {
            _EmployeeList = null;
            _EmployeeList = await new EmployeeM().GetEmployeeAsync(0, true).ConfigureAwait(false);
            if (_EmployeeList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }
            _SearchValue = "@@ShowAll";
            // TempData used to hold search results temporarily
            TempData["EmpSearchState"] = _SearchValue + "@@@" + _ShowPageNum + "@@@" + _ShowItemPerPage;


        }

        // This method is used to View more details for one customer
        public void OnPostViewDetailsEmp(string SelectedEmpID, int currentPageNum)
        {
            ReadSearchData();
            if (SelectedEmpID != null)
            {
                TempData["ModifyEmployeeID"] = SelectedEmpID;
                TempData["FromMaintenance"] = true;
                TempData["EmpSearchState"] = _SearchValue + "@@@" + currentPageNum + "@@@" + _ShowItemPerPage;
                TempData.Keep("EmpSearchState");
                Response.Redirect("/Account", false);
            }
        }

        public async Task OnPostDeleteEmployeeAsync(int SelectedEmpID)
        {
            /// Extract the login state from the cookie and convert it from Base64String to
            // Login object
            var loginCheck = new LoginCheck(Request.Cookies["Cookie"], false);

            if (CUser.UserID != SelectedEmpID)
            {
                if (await _Employee.DeleteEmployeeAsync(SelectedEmpID).ConfigureAwait(false))
                {
                    ViewData["SuccessMsg"] = "Delete Successful";
                }
                else
                {
                    ViewData["FailedMsg"] = "Delete Failed.";
                }
            }
            else
            {
                ViewData["FailedMsg"] = "Cannot delete your own record. Failed";
            }

            // Read the search data
            ReadSearchData();

            if (_SearchValue == "@@ShowAll")
            {
                await OnPostShowAllEmployeeAsync().ConfigureAwait(false);
                _SearchValue = null;
            }
            else
            {
                // Call for search employee method to retrieve the new informations
                await OnPostSearchEmployeeAsync().ConfigureAwait(false);
            }
        }

        // This method is used to control the pagination for the search result and also
        // the Item shown per page
        public async Task OnPostPageControllerAsync(int ShowPageNum, int currentPageNum, int ShowItemPerPage)
        {
            // Read the search data
            ReadSearchData();
            if (ShowItemPerPage != 0)
            {
                _ShowItemPerPage = ShowItemPerPage;
            }
            // If the "ShowPageNum" is not 0
            if (ShowPageNum != 0)
            {
                // Set the class property "_ShowPageNum" to
                // Parameter received
                _ShowPageNum = ShowPageNum;
            }
            else // else if it is 0
            {
                // Use the second parameter which indicates the
                // current pageNum shown to the user
                _ShowPageNum = currentPageNum;
            }

            if (_SearchValue == "@@ShowAll")
            {
                await OnPostShowAllEmployeeAsync().ConfigureAwait(false);
                _SearchValue = null;
            }
            else
            {
                // Call for search employee method to retrieve the new informations
                await OnPostSearchEmployeeAsync().ConfigureAwait(false);
            }

            // if the value of the "_ShowPageNum" is more than total pages for
            // the current Employee list or if it is 0
            if (_ShowPageNum > Convert.ToInt32(Math.Ceiling((_EmployeeList.Count + 0.0) / _ShowItemPerPage)))
            {
                // Set the "_ShowPageNum" to the max page number allowed for the
                // current customer list
                _ShowPageNum = Convert.ToInt32(Math.Ceiling((_EmployeeList.Count + 0.0) / _ShowItemPerPage));
            }

            // If no page number is specified the go to page 1 (Employee))
            if (_ShowPageNum == 0)
                _ShowPageNum = 1;

        }

        // This method is used to read and delete "EmpSearchState" TempData
        private void ReadSearchData()
        {
            // Check if the cookie holding search box value exists
            if (TempData["EmpSearchState"] != null)
            {
                var SearchState = TempData["EmpSearchState"].ToString().Split("@@@");
                _SearchValue = SearchState[0];
                _ShowPageNum = Convert.ToInt32(SearchState[1]);
            }
        }


    }
}