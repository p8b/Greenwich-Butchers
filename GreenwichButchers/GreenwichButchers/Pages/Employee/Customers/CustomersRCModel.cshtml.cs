using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    [BindProperties]
    public class CustomersRCModel : PageModel
    {
        public CustomerM _Customer { get; set; }
        public List<CustomerM> _CustomerList { get; set; }

        public string _SearchValue { get; set; }
        public int _ShowItemPerPage { get; set; } = 5;
        public int _ShowPageNum { get; set; } = 1;

        public async Task OnGet()
        {
            if (TempData["EmpSearchState"] != null)
            {
                var SearchState = TempData["EmpSearchState"].ToString().Split("@@@");
                _SearchValue = SearchState[0];
                _ShowPageNum = Convert.ToInt32(SearchState[1]);
                _ShowItemPerPage = Convert.ToInt32(SearchState[2]);

                if (_SearchValue == "@@ShowAll")
                {
                    await OnPostShowAllCustomerAsync().ConfigureAwait(false);
                    _SearchValue = null;
                }
                else
                {
                    // Call for search customer method to retrieve the new informations
                    await OnPostSearchCustomerAsync().ConfigureAwait(false);
                }
            }
            TempData.Remove("OrderID");
            TempData.Remove("EmpSearchState");
            TempData.Remove("EmpShowCustomer");
        }

        // This method is used to Search for customers by
        // Name, Surname, Email or Customer ID
        public async Task OnPostSearchCustomerAsync()
        {
            if (_SearchValue == "@@ShowAll")
            {
                _SearchValue = null;
            }

            if (_SearchValue == null)
                return;

            _CustomerList = await new CustomerM().SearchCustomerAsync(_SearchValue).ConfigureAwait(false);
            if (_CustomerList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }

            // TempData used to hold search results temporarily
            TempData["TempSearchValues"] = _SearchValue;
        }
        
        // This method is used to show all the customers on the database
        public async Task OnPostShowAllCustomerAsync()
        {
            _CustomerList = await new CustomerM().GetCustomersAsync(0,true).ConfigureAwait(false);
            if (_CustomerList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }

            // TempData used to hold search results temporarily
            TempData["TempSearchValues"] = "@@ShowAll";
        }

        public async Task OnPostAddNewOrderAsync(int CustomerID)
        {
            var SecureCookie = new CookieEncryptM();
            OrderM Order;
            if (SecureCookie.Read(Request.Cookies["Basket"] ?? ""))
            {
                // Extract the order state from the cookie by passing the
                // cookie to "String64ToObject" method of "ObjectConvert", which
                // will convert the cookie value to an object value. Then
                // convert the object to "Order" object
                Order = (OrderM)new ObjectConvert().String64ToObject(SecureCookie.Base64Value);
            } else
            {
                Order = new OrderM();
            }

            Order._Customer = await new CustomerM().GetOneCustomerAsync(CustomerID).ConfigureAwait(false);

            // If so, Create a base64 string by passing the "_Order" object *********************
            // to "ObjectToString64" method of "ObjectConvert" class
            var Base64 = new ObjectConvert().ObjectToString64(Order);

            // Append the base64 string value of order as "Basket"
            // cookie to the response request
            // this cookie will expire 60 minutes after creation
            if (SecureCookie.WriteRead(Request.Cookies["Basket"], Base64))
            {
                Response.Cookies.Append("Basket"
               , SecureCookie.HashCookieID
               , new CookieOptions
               {
                   Expires = DateTime.Now.AddMinutes(60)
               });
                Response.Redirect("/Shop", false);
            }
        }
        // This method is used to View more details for one customer
        public void OnPostViewDetailsCus(string CustomerID, int currentPageNum)
        {
            if (CustomerID != null)
            {
                // Read the search value cookie
                ReadSearchData();
                TempData["EmpSearchState"] = _SearchValue + "@@@" + currentPageNum + "@@@" + _ShowItemPerPage;
                Response.Redirect("/Customer/Account/Edit/"+ CustomerID, false);
            }
        }

        // This method is used to control the pagination for the search result and also
        // the Item shown per page
        public async Task OnPostPageControllerAsync(int ShowPageNum,int currentPageNum)
        {
            // Read the search value cookie
            ReadSearchData();

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
                await OnPostShowAllCustomerAsync().ConfigureAwait(false);
                _SearchValue = null;
            }
            else
            {
                // Call for search customer method to retrieve the new informations
                await OnPostSearchCustomerAsync().ConfigureAwait(false);
            }

            // if the value of the "_ShowPageNum" is more than total pages for
            // the current customer list or if it is 0
            if (_ShowPageNum > Convert.ToInt32(Math.Ceiling((_CustomerList.Count + 0.0) / _ShowItemPerPage)))
            {
                // Set the "_ShowPageNum" to the max page number allowed for the
                // current customer list
                _ShowPageNum = Convert.ToInt32(Math.Ceiling((_CustomerList.Count + 0.0) / _ShowItemPerPage));
            }

            // If no page number is specified the go to page 1 (Product/Stock (Modify))
            if (_ShowPageNum == 0)
                _ShowPageNum = 1;

        }

        // This method is used to read and delete "TempSearchValues" TempData
        private void ReadSearchData()
        {
            // Check if the cookie holding search box value exists
            if (TempData["TempSearchValues"] != null)
            {
                // Set the current SearchValue property
                _SearchValue = TempData["TempSearchValues"].ToString();
                TempData.Remove("TempSearchValues");
            }
        }
    }
}