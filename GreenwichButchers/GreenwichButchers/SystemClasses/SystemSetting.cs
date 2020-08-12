using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace GreenwichButchers.SystemClasses
{
    internal static class SystemSetting
    {
        // The value of the connection string is set in the Index Controller
        internal static string DbConnectionString { get { return Configuration.GetSection("DBString").Value; } }

        // This property is used to get the web root path (wwwroot)
        internal static string WebRootPath { get { return Hostenv.WebRootPath; } }

        // This is the hosting environment to be used throughout the system
        internal static IHostingEnvironment Hostenv { private get; set; }
        // This is used to receive configuration information from the pipline such as appsettings.json data
        internal static IConfiguration Configuration { private get; set; }
    }
}