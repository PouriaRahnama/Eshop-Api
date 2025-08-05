using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using shop.Service.Infrastructure.Filter;

namespace shop.Service.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<LogFilter> _logger;
        public ErrorHandlerMiddleware(RequestDelegate requestDelegate, ILogger<LogFilter> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IErrorHandler error)
        {

            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
            error.GetError(exceptionHandler.Error);
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsJsonAsync(new
            {
                message = error.ErrorMessage,
                statusCode = error.StatusCode
            });
            // لاگ با Serilog
            _logger.LogError("Unhandled exception: {Message}", new
            {
                message = error.ErrorMessage,
                statusCode = error.StatusCode
            });
        }
    }
}
