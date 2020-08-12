using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Customer
{
    [BindProperties]
    public class AccountRCModel : PageModel
    {
        public CustomerM _Customer { get; set; } = new CustomerM();
        public AddressM _Address { get; set; } = new AddressM();

        public string ShowCustomerDetails { get; private set; }
        public string ShowAddressDetails { get; private set; }
        public bool SubscribeMailing { get; set; }

        public async Task OnGet(string IDFor,int CusID ,int AddID,int OrderID)
        {
            ShowCustomerDetails = "show";
            ShowAddressDetails = "";

            // Extract the login state from the cookie and convert it from Base64String to
            // Login object and set the "hashRefresh" to false
            var user = new LoginCheck(Request.Cookies["Cookie"] ?? "", false);

            switch (CUser.UserRole)
            {
                case "Customer":
                    _Customer = await new CustomerM().GetOneCustomerAsync(CUser.UserID).ConfigureAwait(false);
                    ViewData["CustomerModifyTitle"] = _Customer.Title + " " + _Customer.Name + " " + _Customer.Surname;
                    break;
                case "Staff":
                case "Manager":
                    if (TempData["EmpShowCustomer"] == null)
                    {
                        TempData["EmpShowCustomer"] = CusID;
                    }
                    else
                    {
                        CusID = Convert.ToInt32(TempData["EmpShowCustomer"]);
                    }
                    if (OrderID != 0)
                    {
                        TempData["OrderID"] = OrderID;
                    }

                    _Customer = await new CustomerM().GetOneCustomerAsync(CusID).ConfigureAwait(false);
                    ViewData["CustomerModifyTitle"] = "Customer (" + _Customer?.Title + " " + _Customer?.Name + " " + _Customer?.Surname + ")";
                    TempData.Keep("EmpSearchState");
                    TempData.Keep("OrderID");
                    TempData.Keep("EmpShowCustomer");
                    break;
            }

            if (_Customer?.CustomerID > 0)
            {
                var MailingList = await new MailingListM().Get().ConfigureAwait(false);

                foreach(var item in MailingList)
                {
                    if (item == _Customer?.Email)
                    {
                        SubscribeMailing = true;
                    }
                }
            }

            switch (IDFor)
            {
                case "Edit":
                case null:
                    ViewData["Edit"] = "active";
                    break;
                case "AddressEdit":
                    ViewData["AddressEdit"] = "active";
                    break;
                case "EditAddress":
                    AddressPicker(AddID);
                    ViewData["AddressEdit"] = "active";
                    break;
                case "NewAddress":
                    ShowCustomerDetails = "";
                    ViewData["AddressEdit"] = "active";
                    break;

            }
        }

        private void AddressPicker(int id)
        {
            _Address = new AddressM
            {
                AddressID = id
            };

            foreach (var item in _Customer.AddressList)
            {
                if (item.AddressID == id)
                {
                    _Address.AddressID = item.AddressID;
                    _Address.AddressName = item.AddressName;
                    _Address.FirstLine = item.FirstLine;
                    _Address.SecondLine = item.SecondLine;
                    _Address.City = item.City;
                    _Address.PostCode = item.PostCode;
                }
            }
        }


        public async Task OnPostUpdateCustomerInfoAsync()
        {
            // If Customer info Update returns true
            if (await _Customer.UpdateCustomerInfoAsync().ConfigureAwait(false))
            {
                if (SubscribeMailing)
                {
                    await new MailingListM().Add(_Customer?.Email).ConfigureAwait(false);
                }
                else
                {
                    await new MailingListM().Delete(_Customer?.Email).ConfigureAwait(false);

                }
                ViewData["SuccessMsg"] = "Update Successful";

            }
            else // Update password failed
            {
                ViewData["FailedMsg"] = "Update Failed. Email Registered by another User";

            }
            // Used to load the panel and set the title of the page
            await OnGet("Edit", 0,0,0).ConfigureAwait(false);
        }
        public async Task OnPostDeleteCustomerAsync(int CusID)
        {
            if (await _Customer.DeleteCustomerAsync(CusID).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Delete Successful";
            }
            else
            {

                ViewData["FailedMsg"] = "Delete Failed.";
            }

            ViewData["SuccessRedirect"] = "href=/Customers";
        }

        public async Task OnPostUpdatePasswordAsync(string OldPassword)
        {
            // If UpdatePassword returns true
            if (await _Customer.UpdatePasswordAsync(
                _Customer.CustomerID,
                "Customer",
                OldPassword,
                _Customer.Password
                ).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Password Updated";

            }
            else // Update password failed
            {
                ViewData["FailedMsg"] = "Current Password is wrong";

            }
            // Used to load the panel and set the title of the page
            await OnGet("Edit", 0,0,0).ConfigureAwait(false);
        }


        public async Task OnPostUpdateAddressAsync()
        {
            if (await _Address.UpdateAddressAsync().ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Update Successful";
            }
            else
            {

                ViewData["FailedMsg"] = "Update Failed.";
            }
            // Used to load the panel and set the title of the page
            await OnGet("AddressEdit", 0,0,0).ConfigureAwait(false);
        }

        public async Task OnPostAddAddressAsync(int CusID)
        {
            if (await _Address.AddNewAddressAsync(CusID).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "New Address Successful";
            }
            else
            {

                ViewData["FailedMsg"] = "New Address Failed.";
            }

            ViewData["SuccessRedirect"] = "href=/Customer/Account/AddressEdit";

            // Used to load the panel and set the title of the page
            await OnGet("AddressEdit", 0,0,0).ConfigureAwait(false);
        }

        public async Task OnPostDeleteAddressAsync()
        {
            if (await _Address.DeleteAddressAsync(_Address.AddressID).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Delete Successful";
            }
            else
            {

                ViewData["FailedMsg"] = "Delete Failed.";
            }

            ViewData["SuccessRedirect"] = "href=/Customer/Account/AddressEdit";
        }

    }
}