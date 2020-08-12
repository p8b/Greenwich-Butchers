using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GreenwichButchers.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<string> ImagesFiles { get; set; }
        
        public void OnGet()
        {
            try
            {
                ImagesFiles = new List<string>();
                string imgFolder = SystemSetting.WebRootPath + @"\Images\HomePage\";
                var ImagePath = Directory.GetFiles(imgFolder);
                foreach (var img in ImagePath)
                {
                    var PathArray = img.Split(@"\");
                    ImagesFiles.Add(PathArray[PathArray.Length - 1]);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);

                ImagesFiles = new List<string>();
            }
        }
    }
}