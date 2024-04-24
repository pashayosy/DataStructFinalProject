using System.Collections;
using System.Text;

namespace DB.Models;

public class DatabaseEngine<T> : IEnumerable<T> where T : class
{
    private Table<T> table;
    private Dictionary<string, object> indices;

    public DatabaseEngine()
    {
        table = new Table<T>();
        indices = new Dictionary<string, object>();
    }

    // Method to create a unique index
    public void CreateUniqueIndex<TKey>(string indexName, Func<T, TKey> keySelector)
    {
        var index = new UniqueIndex<TKey, T>(keySelector);
        CreateIndex(index, indexName, keySelector);
    }

    // Method to create a range index
    public void CreateRangeIndex<TKey>(string indexName, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
    {
        var index = new RangeIndex<TKey, T>(keySelector);
        CreateIndex(index, indexName, keySelector);
    }


    // Method to create a non-unique index
    public void CreateNonUniqueIndex<TKey>(string indexName, Func<T, TKey> keySelector)
    {
        var index = new NonUniqueIndex<TKey, T>(keySelector);
        CreateIndex(index, indexName, keySelector);
    }

    private void CreateIndex<TKey>(IIndex<TKey,T> index, string indexName, Func<T, TKey> keySelector)
    {
        foreach (var item in table)
        {
            index.Add(keySelector(item), item);
        }
        indices[indexName] = index;
    }

    // General method to add a record
    public void AddRecord(Guid id, T record)
    {
        table.Add(id, record);
        UpdateIndicesAdd(record);
    }

    public void RemoveRecord(Guid id)
    {
        T oldRecord = table.Get(id);
        table.Remove(id);
        UpdateIndicesRemove(oldRecord);
    }

    public void UpdateRecord(Guid id, T updatedRecord)
    {
        T oldRecord = table.Get(id);
        table.Update(id, updatedRecord);
        UpdateIndicesUpdate(oldRecord, updatedRecord);
    }

    private void UpdateIndicesAdd(T record)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic keySelector = index.KeySelector;
            dynamic key = keySelector(record);
            index.Add(key, record);
        }
    }

    private void UpdateIndicesRemove(T record)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic keySelector = index.KeySelector;
            dynamic key = keySelector(record);
            index.Remove(key, record);
        }
    }

    private void UpdateIndicesUpdate(T record, T updatedRecord)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic keySelector = index.KeySelector;
            dynamic oldKey = keySelector(record);
            dynamic newKey = keySelector(updatedRecord);
            index.Remove(oldKey, record);
            index.Add(newKey, updatedRecord);
        }
    }

    public Guid GetGuidByValue(T value)
    {
        return table.Get(value);
    }

    public T getRecordByGuid(Guid guid)
    {
        return table.Get(guid);
    }

    // Method to get a record by unique index
    public T GetRecordByUniqueIndex<TKey>(string indexName, TKey key)
    {
        if (indices.TryGetValue(indexName, out object index))
        {
            return ((UniqueIndex<TKey, T>)index).Get(key);
        }
        throw new KeyNotFoundException("Index not found");
    }

    // Method to get records by non-unique index
    public IEnumerable<T> GetRecordsByNonUniqueIndex<TKey>(string indexName, TKey key)
    {
        if (indices.TryGetValue(indexName, out object index))
        {
            return ((NonUniqueIndex<TKey, T>)index).Get(key);
        }
        throw new KeyNotFoundException("Index not found");
    }

    // Method to get records by range index
    public IEnumerable<T> GetRecordsByRangeIndex<TKey>(string indexName, TKey key) where TKey : IComparable<TKey>
    {
        if (indices.TryGetValue(indexName, out object index))
        {
            return ((RangeIndex<TKey, T>)index).Get(key);
        }
        throw new KeyNotFoundException("Index not found");
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(table.ToString());

        // foreach (var indexEntry in indices)
        // {
        //     sb.AppendLine(indexEntry.ToString());
        // }
        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return table.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
