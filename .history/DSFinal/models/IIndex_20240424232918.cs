namespace DB.Models;

public interface IIndex<TKey, TValue> : BasicIndex
{
    public void Add(TKey key, TValue value);
    public void Remove(TKey key, TValue value);
    public void Update(TKey key, TValue oldValue, TValue newValue);
}

public interface BasicIndex { }