using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Infrastructure;
using shop.Service.Middleware;
using Microsoft.Extensions.Hosting;

namespace shop.Framework.Infrastructure
{
    public class CommonStartUp:IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSwaggerGen();

            services.AddScoped<IErrorHandler, ErrorHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            IWebHostEnvironment env = app.ApplicationServices.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>

                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1")
                    );
            }
          
            app.UseExceptionHandler(app =>
            {
                //Register Exception Middleware
                app.UseMiddleware<ErrorHandlerMiddleware>();
            });
        }      
    }
}
