using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MiddlewareDemo
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public RequestIPMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                //如果这里redis宕机或者未运行会导致每个请求延迟，不适用。从配置文件中读取
                var distributedCache = context.RequestServices.GetService<IDistributedCache>();
                var result = await distributedCache.GetAsync("IP");
                if (result == null)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    var result1 = await distributedCache.GetAsync("IP");
                    var str = System.Text.Encoding.UTF8.GetString(result1);
                    Debug.WriteLine(str);
                    var ip = context.Connection.RemoteIpAddress.ToString();
                    if (str.Contains(ip))
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 403;
                        return;
                    }
                }
                
            }
            catch(Exception ex)
            {
                await _next.Invoke(context);
            }


        }
    }
    
}
