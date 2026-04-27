using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_ProductManager_WithoutBinding.Models;

namespace Assignment_ProductManager_WithoutBinding.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> LoginAsync(string username, string passwordHash);
        Task<bool> RegisterAsync(User user);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
