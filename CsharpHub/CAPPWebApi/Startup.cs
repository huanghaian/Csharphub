using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAPPWebApi.Backgroups;
using CAPPWebApi.Filters;
using Hangfire;
using Hangfire.Logging.LogProviders;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CAPPWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<MyDbContext>(options => options.UseSqlite("data source = " + "Db" + "/" + "TestWeb.db"));
            var sqliteOptions = new SQLiteStorageOptions();
            SQLitePCL.Batteries.Init();
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings()
                .UseLogProvider(new ColouredConsoleLogProvider())
                .UseSQLiteStorage("Data Source=./Db/Hangfire.db;", sqliteOptions)
            );
            services.AddSingleton(new BackgroundJobServerOptions
            {
                WorkerCount = 1,
                ServerName = "TaskSvcHangfireServer"
            });
            //如果注入的是同一个接口的多个类，通过接口调用是获取的是哪个类的实例？
            //注：默认获取的最后一个类的实例，可以IEnumerable<interface> 获取注入的类型
            services.AddScoped<IDataService, WeaterDataService>();

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseAuthorization();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire",new DashboardOptions() { 
                Authorization = new[] {new TimeAuthorizeFilter() }
            });
            RecurringJob.AddOrUpdate<IDataService>("WeaterDataService", options =>options.RunTask(), "0 0 8 * * ?", System.TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<IDataService>("TodayWeaterDataService", options => options.UpdateWheatherInNight(), "0 0 18 * * ?", System.TimeZoneInfo.Local);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
