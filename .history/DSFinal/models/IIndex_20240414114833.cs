namespace DB.Models;

public interface IIndex<TValue>
{
    public void Add();
    public void Update();
    public void Remove();
    public IEnumerable<TValue> GetAll();

    public TValue Get();
}