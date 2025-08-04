using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace shop.Service.Infrastructure.Filter
{
    public class LogFilter:Attribute,IAsyncActionFilter
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation(context.ActionDescriptor.DisplayName + " Executing");

            var result =  await next();

            _logger.LogInformation(context.ActionDescriptor.DisplayName + " Executed");
        }
    }
}
