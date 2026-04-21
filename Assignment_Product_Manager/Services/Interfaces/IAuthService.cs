using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_Product_Manager.Models;

namespace Assignment_Product_Manager.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> LoginAsync(string username, string password);
        Task<(bool Success, string Message)> RegisterAsync(string username, string password,string email, string fullName, string phone);
        void Logout();
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
    }
}
