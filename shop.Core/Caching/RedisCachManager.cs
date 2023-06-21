using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace shop.Core.Caching
{
    public class RedisCachManager:ICacheManager
    {
        #region Filed
        private readonly IDistributedCache _distributedCache;
        public RedisCachManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion


        public T Get<T>(string key)
        {
            string data = _distributedCache.GetString(key);
            if (string.IsNullOrEmpty(data))
            {
                return default(T);
            }

            var ob = JsonConvert.DeserializeObject<T>(data);
            if (ob == null)
                return default(T);

            return ob;
        }



        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(cacheTime));
                var stringData = JsonConvert.SerializeObject(data);
                _distributedCache.SetString(key, stringData, options);
            }
        }



        public bool IsSet(string key)
        {
            return _distributedCache.Get(key) != null;
        }



        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }


    }
}
