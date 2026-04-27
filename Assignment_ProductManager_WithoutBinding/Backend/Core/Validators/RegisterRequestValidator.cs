using Assignment_ProductManager_WithoutBinding.Core.Helpers;
using Assignment_ProductManager_WithoutBinding.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.Core.Validators
{
    public static class RegisterRequestValidator
    {
        public static string? Validate(RegisterRequest request)
        {
            if (!ValidationHelper.IsValidUsername(request.Username))
                return "Username must be 3–30 characters (letters, numbers, underscore only).";

            if (!ValidationHelper.IsValidPassword(request.Password))
                return "Password must be at least 6 characters.";

            if (!ValidationHelper.IsValidEmail(request.Email))
                return "Enter a valid email address.";

            if (string.IsNullOrWhiteSpace(request.FullName))
                return "Full name is required.";

            return null;
        }
    }
}
