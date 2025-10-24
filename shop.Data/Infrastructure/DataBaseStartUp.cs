using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Infrastructure;
using shop.Data.ApplicationContext;
using shop.Data.Persistent.Dapper;
using shop.Data.Repository;


namespace shop.Data.Infrastructure
{
    public class DataBaseStartUp : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.AboveNormal;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = "Data Source=.\\SQL2019;Initial Catalog=EShopApp;Integrated Security=true;Encrypt=false;MultipleActiveResultSets=true";
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddDbContextPool<IApplicationContext, SqlServerApplicationContext>((options) =>
            {

                options.UseSqlServer(connectionString).UseLazyLoadingProxies();
            }, poolSize: 16);

            services.AddTransient(_ => new DapperContext(connectionString));
        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
