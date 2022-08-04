using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Dashome.Core.Extensions;

public static class DistributedCacheExtensions
{
    public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key,
        Func<Task<T>> factory, DistributedCacheEntryOptions options,
        CancellationToken cancellationToken = default) where T : class
    {
        string cached = await cache.GetStringAsync(key, cancellationToken);
        if (cached != null)
        {
            return JsonSerializer.Deserialize<T>(cached);
        }

        T result = await factory();
        await cache.SetStringAsync(key, JsonSerializer.Serialize(result), options, cancellationToken);
        return result;
    }

    public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key,
        Func<T> factory, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default)
        where T : class
    {
        string cached = await cache.GetStringAsync(key, cancellationToken);
        if (cached != null)
        {
            return JsonSerializer.Deserialize<T>(cached);
        }

        T result = factory();
        await cache.SetStringAsync(key, JsonSerializer.Serialize(result), options, cancellationToken);
        return result;
    }

    public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key,
        Func<Task<T>> factory,
        CancellationToken cancellationToken = default) where T : class
    {
        return await cache.GetOrCreateAsync(key, factory, new DistributedCacheEntryOptions(),
            cancellationToken);
    }

    public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<T> factory,
        CancellationToken cancellationToken = default) where T : class
    {
        return await cache.GetOrCreateAsync(key, factory, new DistributedCacheEntryOptions(),
            cancellationToken);
    }

    public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key,
        CancellationToken cancellationToken = default) where T : class
    {
        string cached = await cache.GetStringAsync(key, cancellationToken);
        return cached != null ? JsonSerializer.Deserialize<T>(cached) : null;
    }

    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value,
        CancellationToken cancellationToken = default) where T : class
    {
        await cache.SetStringAsync(key, JsonSerializer.Serialize(value), cancellationToken);
    }
}