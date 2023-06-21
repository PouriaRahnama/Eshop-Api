using Microsoft.AspNetCore.Builder;
using shop.Core.Extension;
using shop.Core.Infrastructure;

namespace shop.Framework.Infrastructure.Extension
{
    public static class ApplicationStartConfig
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            var list = typeof(IApplicationStartup).GetAllClassTypes();
            List<IApplicationStartup> listObject = new List<IApplicationStartup>();

            foreach (var TypeItem in list)
            {
                var ob = Activator.CreateInstance(TypeItem) as IApplicationStartup;
                listObject.Add(ob);
            }

            foreach (var item in listObject)
            {
                item.Configure(application);
            }
        }

    }
}
