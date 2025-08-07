using Serilog;
using Serilog.Events;

public static class SerilogConfig
{
    public static void ConfigureLogging(ConfigureHostBuilder hostBuilder, string appName)
    {
        hostBuilder.UseSerilog((context, logger) =>
        {
            var env = context.HostingEnvironment.EnvironmentName;
            var isDevelopment = context.HostingEnvironment.IsDevelopment();

            logger.MinimumLevel.Is(isDevelopment ? LogEventLevel.Verbose : LogEventLevel.Information)

                .WriteTo.Console()

                // Information logs
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.Async(a => a.File(
                        path: $"Logs/Info/{appName}-info-{env}-{{Date}}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )))

                // Warning logs
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.Async(a => a.File(
                        path: $"Logs/Warning/{appName}-warning-{env}-{{Date}}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )))

                // Error logs
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.Async(a => a.File(
                        path: $"Logs/Error/{appName}-error-{env}-{{Date}}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    )));
        });
    }
}
