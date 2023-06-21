using System.Globalization;
using System.Reflection;
using System.Runtime.Loader;


namespace shop.Core.Extension
{
    public static class Extension
    {
       //
        public static List<string> GetAllClassName(this Type type)
        {
            var _lista = new List<Assembly>();
            foreach (string dllPath in Directory.GetFiles(System.AppContext.BaseDirectory, "shop.*.dll"))
            {
                var shadowCopiedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                _lista.Add(shadowCopiedAssembly);
            }

            return _lista.SelectMany(x => x.GetTypes())
                .Where(x => type.IsAssignableFrom(x) & !x.IsInterface & !x.IsAbstract)
                .Select(x => x.FullName).ToList();
        }



        //
        public static List<Type> GetAllClassTypes(this Type type)
        {
            var _lista = new List<Assembly>();
            foreach (string dllPath in Directory.GetFiles(System.AppContext.BaseDirectory, "shop.*.dll"))
            {
                var shadowCopiedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                _lista.Add(shadowCopiedAssembly);
            }

            return _lista.SelectMany(x => x.GetTypes())
                .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
        }



    }
}
