using System.Data;
using MySql.Data.MySqlClient;

namespace Database;

public class MySqlDatabase
{
    private MySqlConnection Connection;
    private string ConnectionString;

    public MySqlDatabase(string server, string database, string username, string password)
    {
        ConnectionString = $"Server={server};Database={database};User ID={username};Password={password};";
        Connection = new MySqlConnection(ConnectionString);
    }

    private async Task ConnectionOpenAsync()
    {
        if (Connection.State is not ConnectionState.Open)
            await Connection.OpenAsync();
    }
    
    private async Task ConnectionCloseAsync()
    {
        if (Connection.State is not ConnectionState.Closed)
            await Connection.CloseAsync();
    }
    
    private MySqlCommand CreateCommand() => Connection.CreateCommand();
    
    public async Task ExecuteNonQuery(string query, params MySqlParameter[] parameters)
    {
        await ConnectionOpenAsync();
        await using var cmd = CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddRange(parameters);
        cmd.ExecuteNonQuery();
        await ConnectionCloseAsync();
    }
    
    public async Task<MySqlDataReader> ExecuteQueryAsync(string query, params MySqlParameter[] parameters)
    {
        await ConnectionOpenAsync();
        var cmd = CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddRange(parameters);
        var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        return reader;
    }
}