using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SinavOlusturmaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            //// Set up configuration sources.
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json")
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            //migration geldi   
            using (var db = new DatabaseContext())
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(((options => options.EnableEndpointRouting = false)));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(x =>
        {
            x.LoginPath = "/Account/Login/";
        });

            services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
        }
            

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseRouting();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
