using Serilog;
using shop.Framework.Infrastructure.Extension;
using Serilog.Formatting.Compact;
using shop.Service.Extension.FileUtil.Interfaces;
using shop.Service.Extension.FileUtil.Services;
using Shop.Api.Infrastructure.JwtUtil;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices(builder.Configuration);


builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.RegisterApiDependency(builder.Configuration);
builder.Host.UseSerilog((context, logger) =>
{
    logger.MinimumLevel.Verbose() // ÅÊ‘‘ Trace Ê Debug Ê »«·« —
          .WriteTo.Console() // ·«ê »Â ò‰”Ê·
          .WriteTo.File("log.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
          .WriteTo.File(new RenderedCompactJsonFormatter(),
                        "log.ndjson",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error);
});

var app = builder.Build();
app.UseCors("Shop");
app.UseIpRateLimiting();
app.UseStaticFiles();
app.UseDefaultFiles();
app.ConfigureRequestPipeline();
app.Run();
