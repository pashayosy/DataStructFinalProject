using System.Text;

namespace DB.Models;

public class NonUniqueIndex<TKey, TValue> : IIndex<TKey, TValue>
{
    public Func<TValue, TKey> KeySelector { get; }

    private Dictionary<TKey, List<TValue>> index;

    public NonUniqueIndex(Func<TValue, TKey> keySelector)
    {
        index = new Dictionary<TKey, List<TValue>>();
        KeySelector = keySelector;
    }

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

    public override string ToString()
    {
        StringBuilder databaseToString = new StringBuilder();

        foreach (var (keys, values) in index)
        {
            databaseToString.AppendLine($"Key: {keys.ToString()}");
            foreach (var value in values)
            {
                databaseToString.Append($"Object: {value.ToString()}");
            }
        }
        return databaseToString.ToString();
    }
}
