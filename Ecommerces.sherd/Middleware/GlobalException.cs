using Ecommerces.sherd.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Ecommerces.sherd.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string message = "sorry internal server error occurred";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "error";

            try
            {
                await next(context);
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "not found";
                    message = "too meany request made";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(context, statusCode, title, message);

                }
                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "you are not authorized person";
                    await ModifyHeader(context, statusCode, title, message);
                }
                if(context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "uot of access";
                    message = "you are not allowed";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifyHeader(context, statusCode, title, message);
                }

            }
            catch(Exception ex)
            {
                LogExpception.LogExpceptions(ex);
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "out of time";
                    message = "Request to time out ... try again";
                    statusCode = StatusCodes.Status408RequestTimeout;
                }
                await ModifyHeader(context, statusCode, title, message);
            }
        
        }
            
    

        private static async Task ModifyHeader(HttpContext context, int statusCode, string title, string message)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails() { 
            
                Detail = message,
                Title = title,

                Status = statusCode

            }), CancellationToken.None);
            return;

        }
    }
}
