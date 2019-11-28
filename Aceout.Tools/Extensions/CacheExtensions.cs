using Aceout.Tools.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Tools.Extensions
{
    public static class CacheExtensions
    {
        public static void Set<T>(this IDistributedCache cache, string key,  T obj)
        {
            cache.Set(key, SerializationHelper.SerializeBinary(obj));
        }

        public static void Set<T>(this IDistributedCache cache, string key, T obj, DistributedCacheEntryOptions options)
        {
            cache.Set(key, SerializationHelper.SerializeBinary(obj), options);
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T obj)
        {
            return cache.SetAsync(key, SerializationHelper.SerializeBinary(obj));
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T obj, DistributedCacheEntryOptions options)
        {
            return cache.SetAsync(key, SerializationHelper.SerializeBinary(obj), options);
        }

        public static T Get<T>(this IDistributedCache cache, string key)
        {
            var data = cache.Get(key);

            if(data == null || data.Length == 0)
            {
                return default(T);
            }

            return SerializationHelper.DeserializeBinary<T>(data);
        }

        public async static Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var data = await cache.GetAsync(key);

            if (data == null || data.Length == 0)
            {
                return default(T);
            }

            return SerializationHelper.DeserializeBinary<T>(data);
        }
    }
}
