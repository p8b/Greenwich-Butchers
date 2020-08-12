using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    [BindProperties]
    public class AccountRCModel : PageModel
    {
        public EmployeeM _Employee { get; set; }
        public async Task OnGet()
        {
            /// Extract the login state from the cookie and convert it from Base64String to
            // Login object
            var loginCheck = new LoginCheck(Request.Cookies["Cookie"] ?? "", false);

            if (CUser.UserRole == "Manager" || CUser.UserRole == "Staff")
            {
                if (TempData["ModifyEmployeeID"] != null)
                {
                    _Employee = await new EmployeeM().GetOneEmployeeAsync(Convert.ToInt32(TempData["ModifyEmployeeID"])).ConfigureAwait(false);
                    TempData.Keep("EmpSearchState");
                }
                else
                {
                    _Employee = await new EmployeeM().GetOneEmployeeAsync(CUser.UserID).ConfigureAwait(false);
                }
            }
            else
            {
                ViewData["FailedMsg"] = "Cannot delete your own record. Failed";
            }
        }

        public void OnGetNewEmp()
        {
            _Employee = new EmployeeM();
            ViewData["NewEmpAccountTitle"] = "New";
            TempData["FromMaintenance"] = true;
            TempData.Keep("FromMaintenance");
        }
        public async Task OnPostUpdateEmployeeInfoAsync()
        {
            // If Employee info Update returns true
            if (await _Employee.UpdateCustomerInfoAsync().ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Update Successful";

            }
            else // Update password failed
            {
                ViewData["FailedMsg"] = "Update Failed. Email Registered by another User";

            }
            // Used to load the panel and set the title of the page
            await OnGet().ConfigureAwait(false);
        }

        public async Task OnPostUpdatePasswordAsync(string OldPassword)
        {
            // If UpdatePassword returns true
            if (await _Employee.UpdatePasswordAsync(
                _Employee.EmployeeID,
                "Employee",
                OldPassword,
                _Employee.Password
                ).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Password Updated";

            }
            else // Update password failed
            {
                ViewData["FailedMsg"] = "Current Password is wrong";

            }
            // Used to load the panel and set the title of the page
            await OnGet().ConfigureAwait(false);
        }

        public async Task OnPostAddEmployeeInfoAsync()
        {
            // If Employee info Update returns true
            if (await _Employee.AddEmployeeAsync(_Employee).ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Add Successful";
                ViewData["SuccessRedirect"] = "href=/Employees";
            }
            else // Update password failed
            {
                ViewData["FailedMsg"] = "Add Failed. Email Registered by another User";

            }
        }
    }
}