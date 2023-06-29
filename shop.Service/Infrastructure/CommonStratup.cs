using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using shop.Core.Domain.Order;
using shop.Core.Infrastructure;
using shop.Service.Command;
using shop.Service.Infrastructure.Filter;
using shop.Service.Query;
using shop.Service.Query.Comments;
using Shop.Query.Products.GetForShop;

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
            services.AddScoped<CategoryQueryService, CategoryQueryService>();
            services.AddScoped<ProductQueryService, ProductQueryService>();
            services.AddScoped<OrderQueryService, OrderQueryService>();
            services.AddScoped<CommentsQueryService, CommentsQueryService>();
            services.AddScoped<RoleQueryService, RoleQueryService>();
            services.AddScoped<SellerQueryService, SellerQueryService>();
            services.AddScoped<SliderQueryService, SliderQueryService>();
            services.AddScoped<UserQueryService, UserQueryService>();
            services.AddScoped<GetProductsForShopQuery, GetProductsForShopQuery>();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            
        }

       
    }
}
