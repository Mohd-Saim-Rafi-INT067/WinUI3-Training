using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Core.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
            => !string.IsNullOrWhiteSpace(email) &&
               Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static bool IsValidUsername(string username)
            => !string.IsNullOrWhiteSpace(username) &&
               username.Length >= 3 &&
               username.Length <= 30 &&
               Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");

        public static bool IsValidPassword(string password)
            => !string.IsNullOrWhiteSpace(password) && password.Length >= 6;

        public static bool IsValidPrice(string priceText, out decimal price)
        {
            price = 0;
            return decimal.TryParse(priceText, out price) && price > 0 && price <= 9_999_999.99m;
        }

        public static bool IsValidStock(string stockText, out int stock)
        {
            stock = 0;
            return int.TryParse(stockText, out stock) && stock >= 0 && stock <= 100_000;
        }

        public static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLower();
        }

        public static bool VerifyPassword(string password, string hash)
            => HashPassword(password) == hash;
    }
}
