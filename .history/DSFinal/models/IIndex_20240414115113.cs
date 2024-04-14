namespace DB.Models;

public interface IIndex<TKey, TValue>
{
    public void Add(TKey key, TValue value);
    public void Update(TKey key, TValue value);
    public void Remove(TKey key);
    public IEnumerable<TValue> GetAll();

    public TValue Get(TKey key, TValue value);
}