using System.Text;

namespace DB.Models;

public abstract class BaseIndex<TKey, TValue> : IIndex<TKey, TValue>
{
    protected Dictionary<TKey, List<TValue>> index = new Dictionary<TKey, List<TValue>>();
    public Func<TValue, TKey> KeySelector { get; }

    protected BaseIndex(Func<TValue, TKey> keySelector)
    {
        KeySelector = keySelector;
    }

    public void Add(TKey key, TValue value)
    {
        if (!index.ContainsKey(key))
            index[key] = new List<TValue>();
        index[key].Add(value);
    }

    public IEnumerable<TValue> Get(TKey key) => index.TryGetValue(key, out var values) ? values : Enumerable.Empty<TValue>();

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
        var databaseToString = new StringBuilder();
        foreach (var (key, values) in index)
        {
            databaseToString.AppendLine($"\nKey: {key.ToString()}");
            foreach (var value in values)
            {
                databaseToString.Append($"Object: {value.ToString()}");
            }
        }
        return databaseToString.ToString();
    }
}
