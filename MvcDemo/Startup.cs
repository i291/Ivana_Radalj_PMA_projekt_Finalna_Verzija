using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcDemo.DbModels;
using MvcDemo.Repositories;
using MvcDemo.Services;

namespace MvcDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private void SetupDbContext(IServiceCollection service)
        {
            var connString = Configuration.GetConnectionString("t");
            service.AddDbContext<travelingContext>(options => options.UseSqlServer(connString));

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        private void SetupAuthentication(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
               // This lambda determines whether user consent for non-essential cookies is needed for a given request.
               options.CheckConsentNeeded = context => true;
               options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.None;
            });

           
            services.AddAuthentication(o =>
            {
                
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            SetupAuthentication(services);
            services.AddControllersWithViews();
            services.AddScoped<TravelService>();
            services.AddScoped<TravelRepository>();

           
            SetupDbContext(services);


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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
         
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "Putovanja",
                    pattern: "Putovanja/putovanja",
                    defaults: new { controller = "Putovanja", action = "Putovanja" }
                );
                endpoints.MapControllerRoute(
                    name: "Korisnici",
                    pattern: "Korinsici/korisnici",
                    defaults: new { controller = "User", action = "User" }
                );
                endpoints.MapControllerRoute(
                   name: "Korisnici",
                   pattern: "Korinsici/mojaputovanja",
                   defaults: new { controller = "User", action = "mojaputovanja" }
               );

                endpoints.MapControllerRoute("Travellogin",
                   "travel/login",
                   new { controller = "Authentication", action = "login" });

              


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                  name: "Putovanje",
                  pattern: "putovanje",
                  defaults: new { controller = "Putovanja", action = "EditPage" }
              );
            });
        }
    }
}