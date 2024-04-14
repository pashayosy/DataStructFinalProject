namespace DS.Models;

public class Table<T> where T : class
{
    private Dictionary<Guid, T> records;

    public Table() => records = new Dictionary<Guid, T>();

    public void Add(Guid id, T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        records[id] = data;
    }

    public bool Remove(Guid id) => records.Remove(id);

    public T Get(Guid id) => records.TryGetValue(id, out T value) ? value : null;

    public IEnumerable<T> GetAll() => records.Values;

    public void Update(Guid id, T newData)
    {
        if (newData == null)
            throw new ArgumentNullException(nameof(newData));
        if (records.ContainsKey(id))
            records[id] = newData;
        else
            throw new KeyNotFoundException("The specified ID does not exist in the table.");
    }
}
