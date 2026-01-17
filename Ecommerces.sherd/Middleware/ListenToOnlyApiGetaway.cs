using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerces.sherd.Middleware
{
    public class ListenToOnlyApiGetaway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var signedHeader = context.Request.Headers["Api-Getaway"];

            if(signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("sorry service is unavailable");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
