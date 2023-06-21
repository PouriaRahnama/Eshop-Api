using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace shop.Core.Infrastructure
{
    public interface IApplicationStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
        void Configure(IApplicationBuilder app);
        MiddleWarePriority Priority { get; }
    }

        public enum MiddleWarePriority
    {
        Low = 1000,
        BelowNormal = 500,
        Normal = 100,
        AboveNormal = 200,
        High = 100,
        RealTime = 0
    }

}
