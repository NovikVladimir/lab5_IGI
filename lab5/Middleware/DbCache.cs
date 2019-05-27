using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using lab5.ViewModels;

namespace lab5.Middleware
{
    public class DbCache
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;
    
        public DbCache(RequestDelegate next, IMemoryCache memoryCache, string cacheKey = "Operations 10")
        {
            _next = next;
            _memoryCache = memoryCache;
            _cacheKey = cacheKey;
        }
    
        public Task Invoke(HttpContext httpContext)
        {
            //HomeViewModel homeViewModel = null;
            //if (!_memoryCache.TryGetValue(_cacheKey, out homeViewModel))
            //{
            //    homeViewModel = Services.TakeLast.GetHomeViewModel();
            //    _memoryCache.Set(_cacheKey, homeViewModel,
            //        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds((2 * 5) + 240)));
    
            //}
    
            return _next(httpContext);
        }
    }
    
    public static class DbCacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseOperatinCache(this IApplicationBuilder builder, string cacheKey)
        {
            return builder.UseMiddleware<DbCache>(cacheKey);
        }
    }
}
