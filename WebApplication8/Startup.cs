using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data_Access_layer.context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using projectBLL.repo;
using projectBLL.interfaces;
using WebApplication8.mappingProfile;
using data_Access_layer.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebApplication8
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<mvcAppDbcontext>(options =>
            {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));


             });
            // ...existing code...
            services.AddScoped<IunitOfWork, UnitOfWork>();
            services.AddAutoMapper(m => m.AddProfile(new employeeProfile()));
            services.AddAuthentication();
            services.AddIdentity<appUser, IdentityRole>(Options =>
            {
             
            }

            ).AddEntityFrameworkStores<mvcAppDbcontext>().AddDefaultTokenProviders();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=employee}/{action=index}/{id?}");
            });
        }
    }
}
