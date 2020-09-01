using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core_WebApp31
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// Gwneric Web Host Builder
        /// 1. Security
        /// 2. Cookies
        /// 3. Policies
        /// 4. CORS
        /// 5. DI Conainer
        /// 6. Middlewares
        ///     a. HTTPS Redirection
        ///     b. Socket Support through  SignalR
        ///     c. Nginx, socket protocol for Unix Subsystem hosting
        ///     d. HTTP2 protocol
        /// 7. DataAccess Object Management
        /// 8. Caching
        /// 9. Sessions (TempData), internally uses Caching
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // default hosting configuration for the web app using
                    // IConfiguration Contract, load config section from apsettings.json
                    // ConfigureServices() method from  the startp class
                    webBuilder.UseStartup<Startup>();
                });
    }
}
