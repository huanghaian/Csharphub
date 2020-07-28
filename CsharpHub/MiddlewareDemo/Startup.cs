using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "Test";
            });
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.Run(async (context) => {
            //    //Run()中间件，会发生短路，其之后的中间件将不会执行。
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("这是一个中间件:app.Run()");
            //});
            app.UseStaticFiles();
            app.UseRequestIP();
            app.UseRouting();
            app.UseSecurutyHost();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapGet("/GetName/{name:alpha}", async context =>
                {
                    var name = context.Request.RouteValues["name"];
                    context.Response.ContentType = "text/plain;charset=utf-8";
                    await context.Response.WriteAsync($"我的世界：{name}");
                });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=Index}/{id?}");
            });

        }
    }
}
