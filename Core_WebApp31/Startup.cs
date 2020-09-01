using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Core_WebApp31.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core_WebApp31.Models;
using Core_WebApp31.Repositories;
using Core_WebApp31.Services;

namespace Core_WebApp31
{
    public class Startup
    {
        /// <summary>
        /// The Contract with app sttings will be injected in Startup class by using
        /// Default or Generic Host
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// IServiceCollection : The DI Containetr Interface, that uses  'ServiceDescriptor' to perform
        /// depenency lookup and also manage the Depedency Lifetime
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            // register the EAppSoppingContext (Scopped)

            services.AddDbContext<EAppSoppingContext>(options => {
                options.UseSqlServer(
                      Configuration.GetConnectionString("eShoppingDbConnection")
                    ) ;
            });


            // Uawr Based Auth with the User Account Confirmation service
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // The AddIdentity<IdentityUser,IdentityRole>(), used to Userand Role Management

            services.AddIdentity<IdentityUser,IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();


            // define policies for accessing the controllers
            services.AddAuthorization(options => {
                options.AddPolicy("readpolicy", policy =>
                {
                    policy.RequireRole("Manager", "Clerk", "Operator");
                });

                options.AddPolicy("writepolicy", policy =>
                {
                    policy.RequireRole("Manager", "Clerk");
                });
            });

            // register repositories in the DI Contaier

            services.AddScoped<IRepository<Category,int>, CategoryRepository>();
            services.AddScoped<IRepository<Product, int>, ProductRepository>();
            
            // ASP.NET Core 3.X
            
            services.AddControllersWithViews()
               .AddJsonOptions(options=> {
                   options.JsonSerializerOptions.PropertyNamingPolicy = null;
               }); // MVC + API 
            services.AddRazorPages(); // WEB Forms, Databinding (Used by default for Identity Users for User Management)
            // services.AddControllers(); // API
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Method to Manage HTTP Request Processing under 'HttpContext'
        /// The Thread in which the HTTP Pipeline is executed (request+response)
        /// Middlewares, those will be included in the HttpContext and invoked using
        /// the 'RequestDelegate'. They are Custom Classes having RequestDelegate is ctor injected
        /// and containing Invoke() / InvokeAsync() method that contians loginc for Middleware
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // developer exception
                app.UseDatabaseErrorPage(); // standard db error
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // error page in production
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // w3c
            }
            app.UseHttpsRedirection(); // default ttps Redirection
            app.UseStaticFiles(); // js,css and image files

            app.UseRouting(); // HTTP Reouting comon for MVC and API

            // sessions, cors

            app.UseAuthentication(); // User Auth.
            app.UseAuthorization(); // Role-Based Security + Policy Based Auth + OpenIdConnect

            // custom Middleware execution

            // Request Processing for Resource (MVC Controller / API Conroller / Page)
            // UseEndpoints() --> map with reverse proxy for acceoting requests and sending responses
            app.UseEndpoints(endpoints =>
            {
                // MVC Controller
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Razor Web Forms
                endpoints.MapRazorPages();
            });
        }
    }
}
