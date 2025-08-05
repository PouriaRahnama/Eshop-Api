using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace shop.Service.Infrastructure.Filter
{
    public class LogFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionName = context.ActionDescriptor.DisplayName;

            // لاگ شروع اکشن با ورودی‌ها
            var argsJson = JsonSerializer.Serialize(context.ActionArguments);
            _logger.LogInformation("Starting {ActionName} with arguments: {Arguments}", actionName, argsJson);

            var stopwatch = Stopwatch.StartNew();

            // اجرای اکشن
            var resultContext = await next();

            stopwatch.Stop();

            // لاگ پایان اکشن با مدت زمان اجرا و StatusCode
            var statusCode = resultContext.HttpContext.Response.StatusCode;
            _logger.LogInformation("Finished {ActionName} with StatusCode {StatusCode} in {Elapsed} ms",
                actionName, statusCode, stopwatch.ElapsedMilliseconds);
        }
    }
}
