using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DssApplicationPoster.DatabaseManagement;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace DssApplicationPoster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Constants.ConnsStrings = configuration.GetConnectionString("MysqlConnection");
            Constants.Authority = configuration.GetSection("AuthServer").GetValue<string>("Local");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 6_442_450_944; // Size Request Limit 6GB
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            );

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseMvc(route =>
            {
                // Administrator Area Routing
                route.MapAreaRoute(
                    name: "loginAdmin",
                    areaName: "Administrator",
                    template: "Administrator/{controller=Privileged}/{action=Login}");

                route.MapAreaRoute(
                    name: "registerAdmin",
                    areaName: "Administrator",
                    template: "Administrator/{controller=Privileged}/{action=Register}");

                route.MapAreaRoute(
                    name: "dashboardHome",
                    areaName: "Administrator",
                    template: "Administrator/{controller=Dashboard}/{action=Home}");

                route.MapAreaRoute(
                    name: "dashboardUploadImage",
                    areaName: "Administrator",
                    template: "Administrator/{controller=Dashboard}/{action=UploadImage}");

                route.MapAreaRoute(
                    name: "dashboardUploadVideo",
                    areaName: "Administrator",
                    template: "Administrator/{controller=Dashboard}/{action=UploadVideo}");

                // Main Routing
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
