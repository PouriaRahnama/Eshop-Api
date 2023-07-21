using AspNetCoreRateLimit;
using Shop.Api.Infrastructure.JwtUtil;

namespace Shop.Api.Infrastructure.JwtUtil;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddTransient<CustomJwtValidation>();

        service.AddCors(options =>
        {
            options.AddPolicy(name: "Shop",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod();
                });
        });
        service.AddMemoryCache();

        //load general configuration from appsettings.json
        service.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        //load ip rules from appsettings.json
        service.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

        // inject counter and rules stores
        service.AddInMemoryRateLimiting();

        service.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }
}