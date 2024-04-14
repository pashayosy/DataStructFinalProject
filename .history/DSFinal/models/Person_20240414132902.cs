namespace DB.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public Person(int id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Surname} {Email}";
    }
}
