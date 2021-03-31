using AppAfpaBrive.BOL;
using AppAfpaBrive.DAL;
using AppAfpaBrive.DAL.Layers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAfpaBrive.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            InsertInsertionDataLayer obj = new InsertInsertionDataLayer(new AFPANADbContext());
            

                CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging=>
            { logging.AddEventLog(eventLogSettings =>
             {
                 eventLogSettings.SourceName = "AppAfpaBrive";
                 eventLogSettings.LogName = "LogAfpa";
             });
            })
            .ConfigureAppConfiguration((hostContext, builder) =>
            {
                // Add other providers for JSON, etc.

                if (hostContext.HostingEnvironment.IsDevelopment())
                {
                    builder.AddUserSecrets<Program>();
                }
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
