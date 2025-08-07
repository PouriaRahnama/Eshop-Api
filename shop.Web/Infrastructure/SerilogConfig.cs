using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

public static class SerilogConfig
{
    public static void ConfigureLogging(ConfigureHostBuilder hostBuilder, string appName)
    {
        hostBuilder.UseSerilog((context, services, logger) =>
        {
            var env = context.HostingEnvironment.EnvironmentName;
            var isDevelopment = context.HostingEnvironment.IsDevelopment();

            logger.MinimumLevel.Is(isDevelopment ? LogEventLevel.Verbose : LogEventLevel.Information)

                .Enrich.FromLogContext()
                .Enrich.With(new UserInfoEnricher(services.GetRequiredService<IHttpContextAccessor>()))

                // Console log (text)
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} (User: {UserName}, IP: {IPAddress}){NewLine}{Exception}")

                // Information logs - JSON
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.Async(a => a.File(
                        formatter: new JsonFormatter(),
                        path: $"Logs/Info/{appName}-info-{env}-{{Date}}.json",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )))

                // Warning logs - JSON
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.Async(a => a.File(
                        formatter: new JsonFormatter(),
                        path: $"Logs/Warning/{appName}-warning-{env}-{{Date}}.json",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )))

                // Error logs - JSON
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.Async(a => a.File(
                        formatter: new JsonFormatter(),
                        path: $"Logs/Error/{appName}-error-{env}-{{Date}}.json",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )));
        });
    }
}
