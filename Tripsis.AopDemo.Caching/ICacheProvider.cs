namespace Tripsis.AopDemo.Caching
{
    using System;

    /// <summary>
    /// Interface for cache provider.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets the specified cache item using the key.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>The object cached with the specified key.</returns>
        object Get(string cacheKey);

        /// <summary>
        /// Inserts the specified item into the cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="item">The item to cache.</param>
        /// <param name="cacheExpiry">The cache expiry timeout.</param>
        void Insert(string cacheKey, object item, TimeSpan cacheExpiry);
    }
}
