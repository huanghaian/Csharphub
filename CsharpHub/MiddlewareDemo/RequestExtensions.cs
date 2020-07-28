using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareDemo
{
    public static class RequestExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
        public static IApplicationBuilder UseSecurutyHost(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityHostMiddleware>("localhost");
        }
    }
}
