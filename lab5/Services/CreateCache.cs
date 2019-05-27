using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab5.Models;
using Microsoft.Extensions.Caching.Memory;
using lab5.Data;
using lab5.ViewModels;

namespace lab5.Services
{
    public class CreateCache
    {
        private IMemoryCache cache;

        public CreateCache(Context context, IMemoryCache memoryCache)
        {
            cache = memoryCache;
        }

        public HomeViewModel GetProduct(string key)
        {
            HomeViewModel homeViewModel = null;

            if (!cache.TryGetValue(key, out homeViewModel))
            {
                homeViewModel = TakeLast.GetHomeViewModel();
                if (homeViewModel != null)
                {
                    cache.Set(key, homeViewModel,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds((2 * 5) + 240)));
                }
            }
            return homeViewModel;
        }
    }
}
