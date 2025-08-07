using Serilog;
using Serilog.Events;

public static class SerilogConfig
{
    public static void ConfigureLogging(ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, logger) =>
        {
            var environment = context.HostingEnvironment.EnvironmentName;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            logger.MinimumLevel.Verbose()

                .WriteTo.Console()

                // فقط Information (و نه Warning و Error) در Info
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                    .WriteTo.File(
                        path: $"Logs/Info/info-{environment}-{timestamp}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    ))

                // فقط Warning (و نه Error) در Warning
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
                    .WriteTo.File(
                        path: $"Logs/Warning/warning-{environment}-{timestamp}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    ))

                // فقط Error در Error
                .WriteTo.Logger(lc => lc
                    .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
                    .WriteTo.File(
                        path: $"Logs/Error/error-{environment}-{timestamp}.log",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10_000_000,
                        retainedFileCountLimit: 10
                    ));
        });
    }
}
