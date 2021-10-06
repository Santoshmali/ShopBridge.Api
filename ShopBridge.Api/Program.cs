using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    // For text file logging we can use any third pary providers
                    // e.g. https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5
                    logging.ClearProviders();
                    logging.AddConsole();
                });

        // ConfigureWebHostDefaults:
        //     The following defaults are applied to the Microsoft.Extensions.Hosting.IHostBuilder:
        //     • use #####-Kestrel-#### as the web server and configure it using the application's configuration providers
        //     • configure Microsoft.AspNetCore.Hosting.IWebHostEnvironment.WebRootFileProvider to include static web assets from projects referenced by the entry assembly during development
        //     • adds the HostFiltering middleware
        //     • adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true,
        //     • enable IIS integration

        
    }
}
