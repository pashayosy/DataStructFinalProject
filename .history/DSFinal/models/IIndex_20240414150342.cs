namespace DB.Models;

public interface IIndex<TKey, TValue>
{
    public IEnumerable<TValue> Get(TKey key, TValue value);
    public void Add(TKey key, TValue value);
    public void Remove(TKey key, TValue value);
    public void Update(TKey key, TValue oldValue, TValue newValue);
}