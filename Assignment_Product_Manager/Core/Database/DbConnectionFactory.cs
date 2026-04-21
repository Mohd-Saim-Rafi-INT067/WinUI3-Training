using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Core.Database
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<MySqlConnection> CreateConnectionAsync()
        {
            try
            {
                var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB CONNECTION ERROR: " + ex.ToString());
                throw new Exception("Failed to connect to database. Check if MySQL is running and connection string is correct.", ex);
            }
        }
    }
}
