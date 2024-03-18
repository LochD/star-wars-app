using MySql.Data.MySqlClient;
using StarWarsApp.Dtos;

namespace StarWarsApp.Repositories;

public class PersonRepository : IDisposable, IAsyncDisposable
{
    private readonly string connectionString;
    private MySqlCommand command;

    public PersonRepository()
    {
        var server = "127.0.0.1";
        var database = "starwarsdB";
        var username = "root";
        var password = "";
        connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        this.command = connection.CreateCommand();
    }

    public void AddPerson(Person person)
    {
        this.command.CommandText = "INSERT INTO Person (Name, Surname) VALUES (@Name, @Surname)";
        this.command.Parameters.AddWithValue("@Name", person.Name);
        this.command.Parameters.AddWithValue("@Surname", person.Surname);
        this.command.ExecuteNonQuery();
    }
    
    public Person GetPerson(string surname)
    {
        this.command.
    }

    public void Dispose()
    {
        command.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await command.DisposeAsync();
    }
}