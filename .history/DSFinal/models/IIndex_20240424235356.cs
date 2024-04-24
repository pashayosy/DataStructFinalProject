namespace DB.Models;

public interface IIndex<TKey, TValue> : IBasicIndex
{
    public void Add();
    public void Add(TKey key, TValue value);
    public void Remove(TKey key, TValue value);
    public void Update(TKey key, TValue oldValue, TValue newValue);

    public IEnumerable<TValue> Get(TKey key);
}

public interface IBasicIndex
{
    public IEnumerable<object> Get(object key);
}