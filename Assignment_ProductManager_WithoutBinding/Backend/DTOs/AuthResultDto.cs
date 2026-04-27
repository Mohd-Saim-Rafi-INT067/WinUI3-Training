using Assignment_ProductManager_WithoutBinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.DTOs
{
    public class AuthResultDto
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
        public User? User { get; init;}

        public static AuthResultDto Ok(string message, User? user = null) => new() { Success = true, Message = message, User = user };

        public static AuthResultDto Fail(string message) => new() { Success = false, Message = message };

    }
}
