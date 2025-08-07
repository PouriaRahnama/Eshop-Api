using Serilog.Core;
using Serilog.Events;
using System.Security.Claims;

public class UserInfoEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserInfoEnricher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var context = _httpContextAccessor.HttpContext;

        string userName = "Anonymous";
        string ipAddress = "Unknown";

        if (context != null)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                userName = context.User.FindFirst(ClaimTypes.Name)?.Value ?? "UnknownUser";
            }

            ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "UnknownIP";
        }

        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("UserName", userName));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("IPAddress", ipAddress));
    }
}
