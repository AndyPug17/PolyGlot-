using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace PolyGlot.Processing
{
    /// <summary>
    /// Модуль 4: Сервис кэширования и оптимизации
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, DateTime> _keys;
        private readonly int _maxSize;
        
        public CacheService(int maxSize = 1000)
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _keys = new ConcurrentDictionary<string, DateTime>();
            _maxSize = maxSize;
        }
        
        public async Task<T> GetAsync<T>(string key)
        {
            await Task.Delay(0); // For async pattern
            
            if (_memoryCache.TryGetValue(key, out T value))
            {
                UpdateAccessTime(key);
                return value;
            }
            
            return default;
        }
        
        public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
        {
            // LRU eviction if needed
            if (_keys.Count >= _maxSize)
            {
                EvictLeastRecentlyUsed();
            }
            
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = ttl,
                SlidingExpiration = TimeSpan.FromHours(6)
            };
            
            _memoryCache.Set(key, value, cacheOptions);
            _keys.TryAdd(key, DateTime.UtcNow);
            
            await Task.CompletedTask;
        }
        
        public async Task<bool> InvalidateAsync(string key)
        {
            _memoryCache.Remove(key);
            _keys.TryRemove(key, out _);
            
            return await Task.FromResult(true);
        }
        
        public async Task<CacheStatistics> GetStatisticsAsync()
        {
            return await Task.FromResult(new CacheStatistics
            {
                Size = _keys.Count,
                MaxSize = _maxSize,
                HitRate = 0.85 // Simplified for demo
            });
        }
        
        private void UpdateAccessTime(string key)
        {
            _keys.AddOrUpdate(key, DateTime.UtcNow, (k, v) => DateTime.UtcNow);
        }
        
        private void EvictLeastRecentlyUsed()
        {
            var oldestKey = _keys.OrderBy(x => x.Value).FirstOrDefault().Key;
            if (oldestKey != null)
            {
                _memoryCache.Remove(oldestKey);
                _keys.TryRemove(oldestKey, out _);
            }
        }
    }
    
    public class CacheStatistics
    {
        public int Size { get; set; }
        public int MaxSize { get; set; }
        public double HitRate { get; set; }
    }
}
