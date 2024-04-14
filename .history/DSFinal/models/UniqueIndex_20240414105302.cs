using System.Collections;

namespace DB.Models;

public class UniqueIndex<TKey, TValue>
{
    public delegate TValue ChangeFields(TValue value);

    private Dictionary<TKey, TValue> index = new Dictionary<TKey, TValue>();

    public void Add(TKey key, TValue value)
    {
        if (index.ContainsKey(key))
            throw new ArgumentException("Index already contains", nameof(key));
        index[key] = value;
    }

    public TValue Get(TKey key) => index.TryGetValue(key, out TValue value) ? value : throw new ArgumentException($"Value with this key {key} is not exist");

    public void Remove(TKey key) => index.Remove(key);

    public void Update(TKey key, TValue newValue)
    {
        if (index.ContainsKey(key))
            index[key] = newValue;
        else
            throw new KeyNotFoundException("The specified key does not exist in the index.");
    }

    public void Update(TKey key, ChangeFields changeFields)
    {
        if (index.ContainsKey(key))
            index[key] = changeFields(index[key]);
        else
            throw new KeyNotFoundException("The specified key does not exist in the index.");
    }

    public IEnumerable<TValue> GetAll() => index.Values;
}
