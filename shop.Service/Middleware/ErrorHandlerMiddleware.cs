using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace shop.Service.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context,IErrorHandler error)
        {
            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
            error.GetError(exceptionHandler.Error) ;
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsync(error.ErrorMessage);


        }
    }
}
