namespace Assignment.Application.Common.Cashing;
public class CacheEntry<T>
{
    public T Value { get; set; }
    public DateTime Timestamp { get; set; }

    public CacheEntry(T value)
    {
        Value = value;
        Timestamp = DateTime.Now;
    }
}
