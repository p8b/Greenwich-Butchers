using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Customer
{
    [BindProperties]
    public class RegisterRCModel : PageModel
    {
        public CustomerM _Customer { get; set; }
        public AddressM _Address { get; set; }
        public bool AgreeTerms { get; set; }
        public bool SubscribeMailing { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPostRegisterCustomerAsync()
        {
            var user = new LoginCheck(Request.Cookies["Cookie"], false);

            if (await _Customer.AddCustomerAsync(_Customer, _Address).ConfigureAwait(false))
            {
                if (SubscribeMailing)
                {
                    await new MailingListM().Add(_Customer.Email).ConfigureAwait(false);
                }

                switch (CUser.UserRole)
                {
                    case "Manager":
                    case "Staff":
                        ViewData["SuccessMsg"] = "Registration Successful.";
                        ViewData["SuccessRedirect"] = "href=/Customers";
                        break;
                    default:
                        TempData["CustomerReg"] = "SuccessCustomerReg=Registration Successful. Please login here.";
                        Response.Redirect("/Login", false);
                        break;
                }
            }
            else
            {
                switch (CUser.UserRole)
                {
                    case "Manager":
                    case "Staff":
                        ViewData["FailedMsg"] = "Registration Failed." +
                        " Email address is already registered";
                        break;
                    default:
                        TempData["CustomerReg"] = "FailedCustomerReg=Registration Failed." +
                        " Email address is already registered";
                        break;
                }
            }

            ViewData["PanelControllE"] = "ShowPanel2";
        }
    }
}