using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Employee
{
    public class BusinessStatsRCModel : PageModel
    {
        public decimal TotalSale { get; private set; }
        public decimal TotalProfit { get; private set; }
        public decimal TotalSupplierPayment { get; private set; }
        public List<string> MailingList { get; private set; }
        public async Task OnGetAsync()
        {
            var PaymentList = await new PaymentM().GetAsync("Off",0).ConfigureAwait(false);

            if (PaymentList != null)
            {
                foreach(var item in PaymentList)
                {
                    TotalSale = Math.Round(item.TotalPrice + TotalSale, 2);
                    TotalProfit = Math.Round(item.TotalProfit + TotalProfit, 2);
                    TotalSupplierPayment = Math.Round(item.SupplierTotalPayment + TotalSupplierPayment, 2);
                }
            }

            MailingList = await new MailingListM().Get().ConfigureAwait(false);

        }
    }
}