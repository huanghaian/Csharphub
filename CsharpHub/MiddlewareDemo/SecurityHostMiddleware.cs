using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiddlewareDemo
{
    public class SecurityHostMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SecurityHostMiddleware> _logger;
        private readonly string _hosts;

        public SecurityHostMiddleware(RequestDelegate next, ILogger<SecurityHostMiddleware> logger, string hosts)
        {
            _next = next;
            _logger = logger;
            _hosts = hosts;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.ToLower() == "get")
            {
                var hostArray = _hosts.Split(";").ToList();
                if (context.Request.Query.ContainsKey("target"))
                {
                    var url = new Uri(context.Request.Query["target"]);
                    if (!hostArray.Contains(url.Host))
                    {
                        context.Response.Redirect("/home/RedirectSecurityPage?link="+WebUtility.UrlEncode(url.ToString()));
                    }
                    else
                    {
                        await _next.Invoke(context);
                    }

                }
                else
                {
                    await _next.Invoke(context);

                }

            }
            else
                await _next.Invoke(context);
        }
    }
}
