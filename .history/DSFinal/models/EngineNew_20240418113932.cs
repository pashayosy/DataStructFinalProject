using System.Collections;
using System.Text;

namespace DB.Models.New;

public class DatabaseEngine<T> : IEnumerable<Item<T>> where T : class
{
    private Dictionary<Guid, Item<T>> table;
    private Dictionary<string, object> indices;

    public DatabaseEngine()
    {
        table = new Dictionary<Guid, Item<T>>();
        indices = new Dictionary<string, object>();
    }

    // Methods to create indices need adjustment to handle Item<T>
    public void CreateUniqueIndex<TKey>(string indexName, Func<Item<T>, TKey> keySelector)
    {
        var index = new UniqueIndex<TKey, Item<T>>(keySelector);
        foreach (var item in table.Values)
        {
            index.Add(keySelector(item), item);
        }
        indices[indexName] = index;
    }

    public void CreateNonUniqueIndex<TKey>(string indexName, Func<Item<T>, TKey> keySelector)
    {
        var index = new NonUniqueIndex<TKey, Item<T>>(keySelector);
        foreach (var item in table.Values)
        {
            index.Add(keySelector(item), item);
        }
        indices[indexName] = index;
    }

    public void CreateRangeIndex<TKey>(string indexName, Func<Item<T>, TKey> keySelector) where TKey : IComparable<TKey>
    {
        var index = new RangeIndex<TKey, Item<T>>(keySelector);
        foreach (var item in table.Values)
        {
            index.Add(keySelector(item), item);
        }
        indices[indexName] = index;
    }

    public void AddRecord(Guid id, T record)
    {
        var newItem = new Item<T>(id, record);
        table.Add(id, newItem);
        UpdateIndicesAdd(newItem);
    }

    public void RemoveRecord(Guid id)
    {
        if (table.TryGetValue(id, out Item<T> item))
        {
            table.Remove(id);
            UpdateIndicesRemove(item);
        }
    }

    public void UpdateRecord(Guid id, T updatedRecord)
    {
        if (table.TryGetValue(id, out Item<T> oldItem))
        {
            var updatedItem = new Item<T>(id, updatedRecord);
            table[id] = updatedItem;
            UpdateIndicesUpdate(oldItem, updatedItem);
        }
    }

    private void UpdateIndicesAdd(Item<T> item)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic key = index.KeySelector(item);
            index.Add(key, item);
        }
    }

    private void UpdateIndicesRemove(Item<T> item)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic key = index.KeySelector(item);
            index.Remove(key, item);
        }
    }

    private void UpdateIndicesUpdate(Item<T> oldItem, Item<T> updatedItem)
    {
        foreach (var indexEntry in indices)
        {
            dynamic index = indexEntry.Value;
            dynamic oldKey = index.KeySelector(oldItem);
            dynamic newKey = index.KeySelector(updatedItem);
            index.Update();
            index.Remove(oldKey, oldItem);
            index.Add(newKey, updatedItem);
        }
    }

    public Item<T> GetRecordByGuid(Guid guid)
    {
        if (table.TryGetValue(guid, out Item<T> item))
        {
            return item;
        }
        throw new KeyNotFoundException("Record not found.");
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in table)
        {
            sb.AppendLine($"Id: {item.Key}, Value: {item.Value.Value}");
        }
        return sb.ToString();
    }

    public IEnumerator<Item<T>> GetEnumerator()
    {
        return table.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
