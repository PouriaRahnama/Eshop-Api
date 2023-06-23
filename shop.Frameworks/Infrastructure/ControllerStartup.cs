using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using shop.Service.Infrastructure.Filter;

namespace shop.Framework.Infrastructure
{
    public class ControllerStartup : IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            IMvcBuilder config = services.AddControllers();
            config.ConfigureApiBehaviorOptions((options) =>
            {           
                options.ClientErrorMapping[500].Title = "درخواست نامعتبر";
                options.ClientErrorMapping[403].Title = "منبع مورد نظر یافت نشد";
                options.InvalidModelStateResponseFactory = (Context) =>
                {
                    var values = Context.ModelState.Values.Where(state => state.Errors.Count != 0)
                        .Select(state => state.Errors.Select(p => new { errorMessage = p.ErrorMessage }));
                    return new BadRequestObjectResult(values);
                };
            });
            
            //Global
            services.AddMvc().AddMvcOptions(c => c.Filters.AddService(typeof(LogFilter)));

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });
        }

       
    }
}
