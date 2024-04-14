using System.Collections;
using System.Text;

namespace DB.Models;

public class Table<T> : IIndex<Guid, T>, IEnumerable<T> where T : class
{
    // public delegate bool Filter(T value);

    // public delegate T ChangeFields(T value);

    // public delegate object Colomn(T value);
    private Dictionary<Guid, T> _data { get; set; }

    public Table() => _data = new Dictionary<Guid, T>();

    public void Add(T data)
    {
        if (data == null)
            throw new ArgumentNullException("The data cant't be null");

        if (!_data.ContainsValue(data))
            _data[IdCreate()] = data;

    }

    public void Add(Guid id, T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        _data[id] = data;
    }

    public bool Remove(Guid id) => _data.Remove(id);

    public T Get(Guid id) => _data.TryGetValue(id, out T value) ? value : null;

    // public void Update(ChangeFields changeFields, Guid key)
    // {
    //     if (_data.ContainsKey(key))
    //     {
    //         _data[key] = changeFields(_data[key]);
    //     }
    // }
    public void Update(Guid id, T newData)
    {
        if (newData == null)
            throw new ArgumentNullException(nameof(newData));
        if (_data.ContainsKey(id))
            _data[id] = newData;
        else
            throw new KeyNotFoundException("The specified ID does not exist in the table.");
    }

    public Guid Get(T data) => _data.Keys.FirstOrDefault((key) => _data[key].Equals(data));

    // public IEnumerable<T> GetAllFiltred(Filter filter) => _data.Values.Where((item) => filter(item));

    // public Guid GetGuid(Filter filter) => Get(_data.Values.FirstOrDefault((item) => filter(item)));

    // public IEnumerable GetAllColumn(Colomn colomn) => _data.Values.AsEnumerable().Select(x => colomn(x));
    public Guid IdCreate() => Guid.NewGuid();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _data.Values)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<Guid> GetKeys() => _data.Keys;

    public override string ToString()
    {
        StringBuilder databaseToString = new StringBuilder();

        foreach (var (keys, values) in _data)
        {
            databaseToString.AppendLine($"Key: {keys.ToString()} Object: {values.ToString()}");
        }
        return databaseToString.ToString();
    }

}
