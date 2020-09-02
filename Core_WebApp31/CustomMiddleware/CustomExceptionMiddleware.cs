using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebApp31.CustomMiddleware
{
    /// <summary>
    /// Class that will show the Error in JSON structure
    /// </summary>
    public class ErrorInfo
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// The class must be injected with RequestDelegate and must contain
    /// Invoke() or InvokeAsync() method that accept the HttpContext object
    /// This method will contains logic for Middleware
    /// </summary>
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // if no exception while executing controller then just 
                // proceed to next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // generate the error code and message
                context.Response.StatusCode = 500;
                string errorMessage = ex.Message;
                // store the code and message in class
                var errorInfo = new ErrorInfo()
                { 
                     ErrorCode = context.Response.StatusCode,
                     ErrorMessage = errorMessage

                };

                // serialize the errorInfo object
                string error = JsonConvert.SerializeObject(errorInfo);
                // write the error response
                await context.Response.WriteAsync(error);
                
            }
        }
    }


    public static class CustomErrorMiddleware
    {
        public static void UseCustomException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
