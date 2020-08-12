using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Public
{
    [BindProperties]
    public class LoginRCModel : PageModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [DataType(DataType.Password),Required]
        public string Password { get; set; }

        public string ErrorMsg { get; set; }

        public void OnGet(string From)
        {
            // Check if the user is redirected from the "Create New Account" Page
            if (TempData["CustomerReg"] != null)
            {
                // If so the message to be displayed in the "CustomerReg" cookie
                // which contains Message[0] = the name of view bag
                // and Message[1] = The message to be displayed
                var Message =TempData["CustomerReg"].ToString().Split("=");
                ViewData[Message[0]] = Message[1];

            }

            if (From == "Basket")
            {
                ErrorMsg = "* You Must Login/Create New Account To Continue With Your Order.";
            }
        }

        // Checks if the login details are valid
        public void OnPostLoginCheck()
        {
            // Check if the email OR password fields are empty
            if (!ModelState.IsValid)
            {
                ErrorMsg = "Invalid login details";
                // return empty to skip the rest of the code
                return;
            }

            // Create a new object of "Login" class
            // Pass the email and password to the "LoginCheck" method
            var login = new LoginM();

            login.LoginCheck(Email, Password);

            // If the login status is true
            if (login.Status)
            {
                var base64Login = new ObjectConvert().ObjectToString64(login);

                var SecureCookie = new CookieEncryptM();
                if (SecureCookie.WriteRead("", base64Login))
                {
                    Response.Cookies.Append("Cookie", SecureCookie.HashCookieID);
                    if (ErrorMsg == "* You Must Login/Create New Account To Continue With Your Order.")
                    {
                        Response.Redirect("/Order/Checkout?handler=ConfirmBasket", false);
                    }
                    else
                    {
                        Response.Redirect("/Index", false);
                    }
                } else
                {
                    ErrorMsg = "Login failed. Cannot hold login state";
                }
            }
            else
            {
                // Display error message received from DB
                // When there is an error "PersonType" variable is used
                // to pass the error message
                ErrorMsg = login.PersonType;
            }
        }

        // This method is used to delete the cookie containing
        // user login state to enable them to logout
        public void OnGetLogout()
        {
            new CookieEncryptM().Delete(Request.Cookies["Cookie"]);
            Response.Cookies.Delete("Cookie", new CookieOptions {Expires= DateTime.Now });
            Response.Redirect("Index", false);
        }

        // This method is used to create new Customer record
        public void OnPostRegisterCustomer()
        {
            // Redirect to Register Customer Page
            Response.Redirect("RegisterCustomer", false);
        }
    }
}