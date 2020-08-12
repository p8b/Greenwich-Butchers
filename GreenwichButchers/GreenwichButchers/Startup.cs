using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenwichButchers
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IRequestCookieCollection CookiesAccess { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime.
        // This method is used to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options => options.MinimumSameSitePolicy = SameSiteMode.None);

            // Add the whole configuration object here.
            services.AddSingleton(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // In developer environment
            if (env.IsDevelopment())
            {
                // show developer exception page for debugging
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //// HTTP Strict Transport Security (HSTS) Only allow https connections
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Allows this application to use authentication
            //app.UseAuthentication();

            app.UseMvc();

            SetSystemSettings(env, Configuration);
        }

        // This method is used to receive information from the hosting environment and configuration
        private void SetSystemSettings(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            // Receive the hosting environment and set it to the static "Hostenv" property of "SystemSetting" class
            // The Hosting environment is used to get access to the "wwwroot" folder on the client side
            SystemSetting.Hostenv = hostingEnvironment;

            // Receive the Configuration and set it to the static "Configuration" variable of "StstemSetting" Class
            // Configuration used to get access to "appsettings.json" file
            // which holds the connection string to the database.
            SystemSetting.Configuration = configuration;
        }
    }
}