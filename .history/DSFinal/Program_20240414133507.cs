using DB.Models;

DatabaseEngine<Person> databaseEngine = new DatabaseEngine<Person>();

databaseEngine.AddRecord(Guid.NewGuid(), new Person("John", "Taker", "dsa@asd.com"));
