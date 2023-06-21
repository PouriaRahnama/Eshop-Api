using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace shop.Core.Caching
{
    public class MemoryCachManager:ICacheManager
    {

        #region Filed
        private readonly IMemoryCache _memoryCach;
        public MemoryCachManager(IMemoryCache memoryCach)
        {
            _memoryCach = memoryCach;
        }
        #endregion


        public T Get<T>(string key)
        {
            return _memoryCach.Get<T>(key);
        }


        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheTime);
                _memoryCach.Set(key, data, cacheEntryOptions);
            }
        }


        public bool IsSet(string key)
        {
            return _memoryCach.TryGetValue(key, out object _);
        }



        public void Remove(string key)
        {
            _memoryCach.Remove(key);
        }


    }
}
