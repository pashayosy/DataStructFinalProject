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

databaseEngine.CreateUniqueIndex<int>("Id", (person) => person.Id);
databaseEngine.CreateNonUniqueIndex<string>("Name", (person) => person.Name);
databaseEngine.CreateRangeIndex<string>("Age", (person) => person.Age);