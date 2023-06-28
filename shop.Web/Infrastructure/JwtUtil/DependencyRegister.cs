using Shop.Api.Infrastructure.JwtUtil;

namespace Shop.Api.Infrastructure.JwtUtil;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection service)
    {
        service.AddTransient<CustomJwtValidation>();

        //service.AddCors(options =>
        //{
        //    options.AddPolicy(name: "ShopApi",
        //        builder =>
        //        {
        //            builder.AllowAnyOrigin()
        //                .AllowAnyMethod();
        //        });
        //});
    }
}