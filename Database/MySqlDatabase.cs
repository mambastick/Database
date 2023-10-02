using System.Data;
using MySql.Data.MySqlClient;

namespace Database
{
    public class MySqlDatabase
    {
        private string ConnectionString;

        public MySqlDatabase(string server, string database, string username, string password)
        {
            ConnectionString = $"Server={server};Database={database};User ID={username};Password={password};";
        }

        public async Task ExecuteNonQueryAsync(string query, params MySqlParameter[] parameters)
        {
            await using MySqlConnection connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            await using MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<MySqlDataReader> ExecuteQueryAsync(string query, params MySqlParameter[] parameters)
        {
            await using MySqlConnection connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            await using MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            
            var reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            return reader;
        }

        public async Task<object> ExecuteScalarAsync(string query, params MySqlParameter[] parameters)
        {
            await using MySqlConnection connection = new MySqlConnection(ConnectionString);
            await connection.OpenAsync();

            await using MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddRange(parameters);
            
            var result = await cmd.ExecuteScalarAsync();
            return result;
        }
    }
}