using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Infrastructure;

namespace shop.Framework.Infrastructure
{
    public class SecurityStartup:IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.High;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
        }

    }
}
