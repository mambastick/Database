using System.Data;
using MySql.Data.MySqlClient;

namespace MDatabase;

public class MDatabase
{
    private readonly string ConnectionString;

    public MDatabase(string server, string database, string username, string password)
    {
        ConnectionString = $"Server={server};Database={database};User ID={username};Password={password};";
    }

    public async Task ExecuteNonQueryAsync(string query, params MySqlParameter[] parameters)
    {
        await using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddRange(parameters);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<MySqlDataReader> ExecuteQueryAsync(string query, params MySqlParameter[] parameters)
    {
        var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddRange(parameters);

        var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        return reader;
    }

    public async Task<object?> ExecuteScalarAsync(string query, params MySqlParameter[] parameters)
    {
        await using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        await using var cmd = connection.CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddRange(parameters);

        return await cmd.ExecuteScalarAsync();
    }

    public async Task<bool> PingAsync()
    {
        var connection = new MySqlConnection(ConnectionString);
        
        try
        {
            await connection.OpenAsync();
            return true;
        }
        catch (MySqlException)
        {
            return false;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}