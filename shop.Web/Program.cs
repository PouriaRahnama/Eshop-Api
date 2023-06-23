using Serilog;
using shop.Framework.Infrastructure.Extension;
using Serilog.Formatting.Compact;
using shop.Service.Extension.FileUtil.Interfaces;
using shop.Service.Extension.FileUtil.Services;
using Shop.Api.Infrastructure.JwtUtil;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Host.UseSerilog(((context, provider, logger) =>
{
    logger.MinimumLevel.Information().WriteTo.File("log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
    ).WriteTo.File(new RenderedCompactJsonFormatter(), "log.ndjson", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning);
}));
var app = builder.Build();
app.UseStaticFiles();
app.UseDefaultFiles();
app.ConfigureRequestPipeline();
app.Run();
