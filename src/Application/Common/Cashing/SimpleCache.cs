namespace Assignment.Application.Common.Cashing;

public class SimpleCache<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, CacheEntry<TValue>> _cache;
    private readonly TimeSpan _cacheDuration;
    private static SimpleCache<TKey, TValue>? _instance;
    private static readonly object _lock = new();

    private SimpleCache(TimeSpan cacheDuration)
    {
        _cache = new Dictionary<TKey, CacheEntry<TValue>>();
        _cacheDuration = cacheDuration;
    }

    public static SimpleCache<TKey, TValue> Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // Set a default cache duration if none is provided
                        _instance = new SimpleCache<TKey, TValue>(TimeSpan.FromMinutes(10));
                    }
                }
            }
            return _instance;
        }
    }

    public TValue? Get(TKey key)
    {
        if (_cache.ContainsKey(key))
        {
            var entry = _cache[key];
            if (DateTime.Now - entry.Timestamp < _cacheDuration)
            {
                return entry.Value;
            }
            else
            {
                _cache.Remove(key);
            }
        }
        return default;
    }

    public void Set(TKey key, TValue value)
    {
        _cache[key] = new CacheEntry<TValue>(value);
    }

    public void Reset(TKey key)
    {
        if (_cache.ContainsKey(key))
        {
            _cache.Remove(key);
        }
    }
}
