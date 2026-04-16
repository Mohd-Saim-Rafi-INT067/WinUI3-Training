using Assignment_Product_Manager.Core.Constants;
using Assignment_Product_Manager.Core.Database;
using Assignment_Product_Manager.Repositories.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_Product_Manager.Models;

namespace Assignment_Product_Manager.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _db;

        public UserRepository(DbConnectionFactory db) => _db = db;

        public async Task<User?> LoginAsync(string username, string passwordHash)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.LoginUser, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_Username", username);
            cmd.Parameters.AddWithValue("p_PasswordHash", passwordHash);

            await using var reader = await cmd.ExecuteReaderAsync();
            //is it returning atleat one row?
            if (await reader.ReadAsync())
            {
                return new User
                {
                    UserId = reader.GetInt32("UserId"),
                    Username = reader.GetString("Username"),
                    Email = reader.GetString("Email"),
                    FullName = reader.GetString("FullName"),
                    PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString("PhoneNumber"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    IsActive = reader.GetBoolean("IsActive")
                };
            }
            return null;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.RegisterUser, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_Username", user.Username);
            cmd.Parameters.AddWithValue("p_PasswordHash", user.Password);
            cmd.Parameters.AddWithValue("p_Email", user.Email);
            cmd.Parameters.AddWithValue("p_FullName", user.FullName);
            cmd.Parameters.AddWithValue("p_PhoneNumber", user.PhoneNumber);

            var result = await cmd.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.CheckUserExists, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_Username", username);
            cmd.Parameters.AddWithValue("p_Email", "");

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.CheckUserExists, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_Username", "");
            cmd.Parameters.AddWithValue("p_Email", email);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }
    }
}
