using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using shop.Core.Infrastructure;
using shop.Data.ApplicationContext;
using shop.Data.Repository;


namespace shop.Data.Infrastructure
{
    public class DataBaseStartUp:IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.AboveNormal;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddDbContextPool<IApplicationContext,SqlServerApplicationContext>((options) =>
            {               
                options.UseSqlServer("Data Source=.;Initial Catalog=ShopApp;Integrated Security=true;Encrypt=false;MultipleActiveResultSets=true").UseLazyLoadingProxies();
            }, poolSize: 16);
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
