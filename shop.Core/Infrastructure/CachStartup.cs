using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Caching;

namespace shop.Core.Infrastructure
{
    public class CachStartup:IApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1:6379";
                option.InstanceName = "test";
            });
            services.AddScoped<ICacheManager, RedisCachManager>();
        }


        public void Configure(IApplicationBuilder app)
        {

        }

        public MiddleWarePriority Priority => MiddleWarePriority.Normal;
    }
}
