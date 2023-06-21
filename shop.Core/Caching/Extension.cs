namespace shop.Core.Caching
{
    public static class Extension
    {


        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> GetFromDb)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }
            var result = GetFromDb();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }




        public static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<Task<T>> GetFromDb)
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = await GetFromDb();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);

            return result;
        }


    }
}
