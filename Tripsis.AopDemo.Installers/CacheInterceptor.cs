namespace Tripsis.AopDemo.Installers
{
    using System;
    using System.Linq;

    using Castle.DynamicProxy;

    using Tripsis.AopDemo.Caching;

    /// <summary>
    /// Interceptor for caching.
    /// </summary>
    public class CacheInterceptor : IInterceptor
    {
        /// <summary>
        /// The cache provider implementation.
        /// </summary>
        private readonly ICacheProvider cacheProvider;

        /// <summary>
        /// Defines whether caching is enabled.
        /// </summary>
        private readonly bool cachingIsEnabled;

        /// <summary>
        /// The cache timeout.
        /// </summary>
        private readonly TimeSpan cacheTimeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheInterceptor" /> class.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <param name="cachingIsEnabled">if set to <c>true</c> caching is enabled.</param>
        /// <param name="cacheTimeoutSeconds">The cache timeout seconds.</param>
        public CacheInterceptor(ICacheProvider cacheProvider, bool cachingIsEnabled, int cacheTimeoutSeconds)
        {
            this.cacheProvider = cacheProvider;
            this.cachingIsEnabled = cachingIsEnabled;
            this.cacheTimeout = TimeSpan.FromSeconds(cacheTimeoutSeconds);
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            // check if the method has a return value
            if (!this.cachingIsEnabled || invocation.Method.ReturnType == typeof(void))
            {
                invocation.Proceed();
                return;
            }

            var cacheKey = BuildCacheKeyFrom(invocation);

            // try get the return value from the cache provider
            var item = this.cacheProvider.Get(cacheKey);

            if (item != null)
            {
                invocation.ReturnValue = item;
                return;
            }

            // call the intercepted method
            invocation.Proceed();

            if (invocation.ReturnValue != null)
            {
                this.cacheProvider.Insert(cacheKey, invocation.ReturnValue, this.cacheTimeout);
            }
        }

        /// <summary>
        /// Builds the cache key from the given method invocation.
        /// </summary>
        /// <param name="invocation">The method invocation.</param>
        /// <returns>A string representation of the invocation.</returns>
        private static string BuildCacheKeyFrom(IInvocation invocation)
        {
            var methodName = invocation.Method.ToString();

            var arguments = invocation.Arguments.Select(a => a.ToString()).ToArray();
            var argsString = string.Join(",", arguments);

            var cacheKey = methodName + "-" + argsString;

            return cacheKey;
        }
    }
}
