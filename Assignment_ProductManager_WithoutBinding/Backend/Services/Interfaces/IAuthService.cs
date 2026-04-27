using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_ProductManager_WithoutBinding.DTOs;
using Assignment_ProductManager_WithoutBinding.Models;

namespace Assignment_ProductManager_WithoutBinding.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(string username, string password);
        Task<AuthResultDto> RegisterAsync(RegisterRequest request);
        void Logout();
        User? CurrentUser { get; }
        bool IsLoggedIn { get; }
    }
}
