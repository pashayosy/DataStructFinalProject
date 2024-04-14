namespace DB.Models;

public class UniqueIndex<TKey, TValue>
{
    private Dictionary<TKey, TValue> index = new Dictionary<TKey, TValue>();

    public void Add(TKey key, TValue value)
    {
        index[key] = value;
    }

    public TValue Get(TKey key) => index.TryGetValue(key, out TValue value) ? value : default;

    public void Remove(TKey key) => index.Remove(key);

    public void Update(TKey key, TValue newValue)
    {
        if (index.ContainsKey(key))
            index[key] = newValue;
        else
            throw new KeyNotFoundException("The specified key does not exist in the index.");
    }
}
