using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CAPPWebApi.Extensions;

namespace CAPPWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //database Directory init
            if(!Directory.Exists("Db")){
                Directory.CreateDirectory("Db");
            }
            CreateHostBuilder(args).Build().MigrateDbContext<MyDbContext>().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:8500") ;
                });
    }
}
