using DB.Models;

DatabaseEngine<Person> databaseEngine = new DatabaseEngine<Person>();

databaseEngine.AddRecord(Guid.NewGuid(), new Person("John", "Flop", "123@123.123"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Toper", "Top", "sdawq@da.dsa"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Toper", "Top", "sdawq@da.dsa"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Toper", "Top", "sdawq@da.dsa"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Foper", "Pop", "sad@sad.sad"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Shoper", "Sker", "dsa@asd.com"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Sloper", "Skew", "987@a987.987"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Ziker", "Flex", "moper@roper.roper"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Tiker", "Flox", "zker@asd.zker"));

Console.WriteLine(databaseEngine);
databaseEngine.CreateUniqueIndex<int>("Id", (person) => person.Id);
databaseEngine.CreateNonUniqueIndex<string>("Name", (person) => person.Name);
databaseEngine.CreateRangeIndex<string>("Surname", (person) => person.Surname);
Console.WriteLine("----------------------Get Record by id----------------------");
Console.WriteLine(databaseEngine.GetRecordByUniqueIndex<int>("Id", 1));
Console.WriteLine("----------------------Get Record by Name----------------------");
foreach (var record in databaseEngine.GetRecordsByNonUniqueIndex<string>("Name", "Toper"))
    Console.WriteLine(record);
Console.WriteLine("----------------------Get Record by id----------------------");
foreach (var record in databaseEngine.GetRecordsByRangeIndex<string>("Surename", "Top"))
    Console.WriteLine(record);
Console.WriteLine("--------------------------------------------");

Console.WriteLine(databaseEngine);

Console.WriteLine("----------------------Add----------------------");
Guid g = Guid.NewGuid();
Person p = new Person("123", "123", "fds@sdf.sdf");
databaseEngine.AddRecord(g, p);
Console.WriteLine(databaseEngine);

Console.WriteLine("----------------------Update----------------------");
databaseEngine.UpdateRecord(g, new Person("ggg", "ggg", "@."));
Console.WriteLine(databaseEngine);

Console.WriteLine("----------------------Remove----------------------");
databaseEngine.RemoveRecord(g);
Console.WriteLine(databaseEngine);

