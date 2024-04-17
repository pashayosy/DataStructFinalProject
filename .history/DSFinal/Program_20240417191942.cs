using DB.Models;

DatabaseEngine<Person> databaseEngine = new DatabaseEngine<Person>();

databaseEngine.AddRecord(Guid.NewGuid(), new Person("John", "Flop", "123@123.123"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Toper", "Top", "sdawq@da.dsa"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Foper", "Pop", "sad@sad.sad"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Shoper", "Sker", "dsa@asd.com"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Sloper", "Skew", "987@a987.987"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Ziker", "Flex", "moper@roper.roper"));
databaseEngine.AddRecord(Guid.NewGuid(), new Person("Tiker", "Flox", "zker@asd.zker"));

Console.WriteLine(databaseEngine);
databaseEngine.CreateUniqueIndex<int>("UniqId", (person) => person.Id);
databaseEngine.CreateNonUniqueIndex<string>("Name", (person) => person.Name);
databaseEngine.CreateRangeIndex<int>("RangeId", (person) => person.Id);

Console.WriteLine(databaseEngine.GetRecordByUniqueIndex<int>("UniqId", 1));
Console.WriteLine(databaseEngine.GetRecordsByNonUniqueIndex<string>("Name", "Tiker"));
Console.WriteLine(databaseEngine.GetRecordsByRangeIndex<int>("RangeId", 3));

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

