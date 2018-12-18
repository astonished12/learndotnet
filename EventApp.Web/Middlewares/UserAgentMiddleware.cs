using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EventApp.Web.Middlewares
{
    public class UserAgentMiddleware
    {
        private readonly RequestDelegate _next;

        public UserAgentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var headers = context.Request.Headers;
            var userAgent = headers["User-Agent"].ToString();

            if ((userAgent.Contains("Trident") || userAgent.Contains("MSIE")) && context.Request.Path != "/User/Agent")
            {
                context.Request.Path = "/User/Agent";
            }

            await _next(context);

        }
    }
}

