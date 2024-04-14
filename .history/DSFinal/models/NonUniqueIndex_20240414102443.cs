namespace DB.Models;

public class NonUniqueIndex<TKey, TValue>
{
    private Dictionary<TKey, List<TValue>> index = new Dictionary<TKey, List<TValue>>();

    public void Add(TKey key, TValue value)
    {
        if (!index.ContainsKey(key))
            index[key] = new List<TValue>();
        index[key].Add(value);
    }

    public IEnumerable<TValue> Get(TKey key) => index.TryGetValue(key, out List<TValue> values) ? values : Enumerable.Empty<TValue>();

    public void Remove(TKey key, TValue value)
    {
        if (index.ContainsKey(key))
        {
            index[key].Remove(value);
            if (index[key].Count == 0)
                index.Remove(key);
        }
    }

    public void Update(TKey key, TValue oldValue, TValue newValue)
    {
        if (index.ContainsKey(key) && index[key].Contains(oldValue))
        {
            index[key].Remove(oldValue);
            index[key].Add(newValue);
        }
        else
        {
            throw new KeyNotFoundException("The specified value for the key does not exist in the index.");
        }
    }
}
