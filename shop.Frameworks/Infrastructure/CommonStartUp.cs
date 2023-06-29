using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Infrastructure;
using Microsoft.Extensions.Hosting;
using shop.Service.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace shop.Framework.Infrastructure
{
    public class CommonStartUp:IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {           
            services.AddSwaggerGen(option =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Enter Token",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                option.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
            });
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
