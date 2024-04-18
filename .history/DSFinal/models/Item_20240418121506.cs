namespace DB.Models;

public class Item<T> where T : class
{
    public Guid Id { get; private set; }
    public T Value { get; private set; }

    public Item(Guid id, T value)
    {
        Id = id;
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
