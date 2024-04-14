namespace DB.Models;

public class Table<T>
{
    private IIndex<int, T> primaryIndex; // Example: Primary index based on an ID

    public void Add(int id, T item)
    {
        primaryIndex.Add(id, item);
        // Add to other indices
    }

    public void Remove(int id)
    {
        primaryIndex.Remove(id);
        // Remove from other indices
    }

    public void Update(int id, T newItem)
    {
        primaryIndex.Update(id, newItem);
        // Update other indices
    }

    public T Get(int id) => primaryIndex.Get(id);
    public IEnumerable<T> GetAll() => primaryIndex.GetAll();
}
