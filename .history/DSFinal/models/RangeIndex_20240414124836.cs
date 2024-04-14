namespace DB.Models;

public class RangeIndex<TKey, TValue> where TKey : IComparable<TKey>
{
    private SortedDictionary<TKey, List<TValue>> index;

    public RangeIndex()
    {
        index = new SortedDictionary<TKey, List<TValue>>();
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

    public IEnumerable<TValue> QueryRange(TKey min, TKey max)
    {
        for (int i = 0; i < 10; i++)
        {

        }


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
}
