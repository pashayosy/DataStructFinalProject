using DB.Models;

TestEngine();
TestNewEngine();

void TestEngine()
{
    Console.WriteLine("----------------------------TESTENGINE------------------------------------");
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
    Person p1 = databaseEngine.GetRecordByUniqueIndex<int>("Id", 1);
    Console.WriteLine(p1);
    Console.WriteLine("----------------------Get Record by Name----------------------");
    foreach (var record in databaseEngine.GetRecordsByNonUniqueIndex<string>("Name", "Toper"))
        Console.WriteLine(record);
    Console.WriteLine("----------------------Get Record by id----------------------");
    foreach (var record in databaseEngine.GetRecordsByRangeIndex<string>("Surname", "Top"))
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
}


void TestNewEngine()
{
    Console.WriteLine("\n\n\n\n\n\n ----------------------------TESTNEWENGINE------------------------------------");

    DB.Models.New.DatabaseEngine<Person> databaseEngine = new DB.Models.New.DatabaseEngine<Person>();

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
    databaseEngine.CreateUniqueIndex<int>("Id", (item) => item.Value.Id);
    databaseEngine.CreateNonUniqueIndex<string>("Name", (item) => item.Value.Name);
    databaseEngine.CreateRangeIndex<string>("Surname", (item) => item.Value.Surname);
    Console.WriteLine("----------------------Get Record by id----------------------");
    Item<Person> item1 = databaseEngine.GetRecordByUniqueIndex<int>("Id", 12);
    Console.WriteLine(item1.Value);
    Console.WriteLine("----------------------Get Record by Name----------------------");
    foreach (var record in databaseEngine.GetRecordsByNonUniqueIndex<string>("Name", "Toper"))
        Console.WriteLine(record.Value);
    Console.WriteLine("----------------------Get Record by id----------------------");
    foreach (var record in databaseEngine.GetRecordsByRangeIndex<string>("Surname", "Top"))
        Console.WriteLine(record.Value);
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
}
