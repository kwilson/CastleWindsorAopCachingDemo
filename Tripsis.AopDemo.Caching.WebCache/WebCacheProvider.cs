namespace Tripsis.AopDemo.Caching.WebCache
{
    using System;
    using System.Web;

    /// <summary>
    /// Implementation of <see cref="ICacheProvider"/> using the web cache.
    /// </summary>
    public class WebCacheProvider : ICacheProvider
    {
        /// <summary>
        /// Gets the specified cache item using the key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>
        /// The object cached with the specified key.
        /// </returns>
        public object Get(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }

        /// <summary>
        /// Inserts the specified item into the cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="item">The item to cache.</param>
        /// <param name="cacheExpiry">The cache expiry timeout.</param>
        public void Insert(string cacheKey, object item, TimeSpan cacheExpiry)
        {
            HttpRuntime.Cache.Insert(
                cacheKey,
                item, 
                null, 
                DateTime.Now + cacheExpiry, 
                TimeSpan.Zero);
        }
    }
}
