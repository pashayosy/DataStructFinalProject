namespace DB.Models;

public class Person
{
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    private static int counter = 0;

    public Person(string name, string surname, string email)
    {
        Id = counter++;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        Person other = (Person)obj;

        if (other == null)
            return false;

        return Id == other.Id && Name == other.Name && Surname == other.Surname && Email == other.Email;
    }

    public override string ToString()
    {
        return $"{Id} {Name} {Surname} {Email}\n";
    }
}
