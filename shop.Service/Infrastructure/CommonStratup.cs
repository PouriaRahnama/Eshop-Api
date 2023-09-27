using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Domain.Order;
using shop.Core.Infrastructure;
using shop.Service.Command;
using shop.Service.Infrastructure.Filter;
using shop.Service.Query;
using FluentValidation.AspNetCore;
using System.Reflection;
using FluentValidation;
using shop.Service.Query.Product.GetForShop;

namespace shop.Service.Infrastructure
{
    public class CommonStratup:IApplicationStartup
    {
        public MiddleWarePriority Priority => MiddleWarePriority.Normal;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<LogFilter>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            //
            services.AddScoped<ICategoryQueryService, CategoryQueryService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped<IOrderQueryService, OrderQueryService>();
            services.AddScoped<ICommentsQueryService, CommentsQueryService>();
            services.AddScoped<IRoleQueryService, RoleQueryService>();
            services.AddScoped<ISellerQueryService, SellerQueryService>();
            services.AddScoped<ISliderQueryService, SliderQueryService>();
            services.AddScoped<IUserQueryService, UserQueryService>();
            services.AddScoped<IGetProductsForShopQuery, GetProductsForShopQuery>();

            services.AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;

                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
        }
        
        public void Configure(IApplicationBuilder app)
        {
            
        }

       
    }
}
