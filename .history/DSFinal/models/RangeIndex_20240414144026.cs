using System.Text;

namespace DB.Models;

public class RangeIndex<TKey, TValue> where TKey : IComparable<TKey>
{
    public Func<TKey, TValue> KeySelector { get; }
    private SortedDictionary<TKey, List<TValue>> index;

    public RangeIndex(Func<TKey, TValue> keySelector)
    {
        index = new SortedDictionary<TKey, List<TValue>>();
        KeySelector = KeySelector;
    }

    public void Add(TKey key, TValue value)
    {
        if (!index.ContainsKey(key))
            index[key] = new List<TValue>();
        index[key].Add(value);
    }

    public void Remove(TKey key, TValue value)
    {
        if (index.ContainsKey(key))
        {
            index[key].Remove(value);
            if (index[key].Count == 0)
                index.Remove(key);
        }
    }

    public IEnumerable<TValue> Get(TKey key) => index.TryGetValue(key, out List<TValue> values) ? values : Enumerable.Empty<TValue>();

    public IEnumerable<TValue> QueryRange(TKey min, TKey max)
    {
        var keysInRange = index.Keys.Where(key => key.CompareTo(min) >= 0 && key.CompareTo(max) <= 0);
        foreach (var key in keysInRange)
        {
            foreach (var value in index[key])
            {
                yield return value;
            }
        }
    }

    public void Update(TKey oldKey, TKey newKey, TValue oldValue, TValue newValue)
    {
        Remove(oldKey, oldValue);
        Add(newKey, newValue);
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
