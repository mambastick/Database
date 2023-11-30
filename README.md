# 🗂️ MDatabase 🗂️

**MDatabase** - This is a library created for simplified developer interaction with the database. 
The library is based on [MySQL.Data](https://www.nuget.org/packages/MySql.Data/)

## How to use ❓
```csharp
// Initialize database
var database = new MDatabase(
    server: localhost,
    database: TestDB,
    username: admin,
    password: 1234,
);

// Insert info to database
await database.ExecuteNonQueryAsync("INSERT INTO Users (Id, Name) VALUES (1, \"Eugene\");");

// Get info from database
var reader = await database.ExecuteQueryAsync("SELECT * FROM Users;");
if (await reader.ReadAsync())
    Console.WriteLine(reader.GetString("Name")); // Eugene
```