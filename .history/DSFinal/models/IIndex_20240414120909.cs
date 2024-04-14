namespace DB.Models;

public interface IIndex<TKey, TValue>
{
    void Add(TKey key, TValue value);
    bool Remove(TKey key);
    bool Update(TKey key, TValue newValue);
    TValue Get(TKey key);
    IEnumerable<TValue> GetAll();
}