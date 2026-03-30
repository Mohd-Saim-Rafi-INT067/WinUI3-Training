using NavigationApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Windows.Storage;

namespace NavigationApp.Services
{
    public static class UserStore
    {
        private static string GetFilePath()
        {
            string folder = ApplicationData.Current.LocalFolder.Path;
            string appFolder = Path.Combine(folder, "NavigationApp");
            Directory.CreateDirectory(appFolder);
            string path = Path.Combine(appFolder, "users.json");
            System.Diagnostics.Debug.WriteLine("UserStore path: " + path);
            return path;
        }

        public static List<User> LoadUsers()
        {
            string path = GetFilePath();

            if (!File.Exists(path))
                return new List<User>();

            string json = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<User>();

            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public static void SaveUsers(List<User> users)
        {
            string path = GetFilePath();
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        public static bool EmailExists(string email)
        {
            var users = LoadUsers();
            return users.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public static bool RegisterUser(User user, out string error)
        {
            error = "";

            var users = LoadUsers();

            bool exists = users.Any(u => string.Equals(u.Email, user.Email, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                error = "Email already registered.";
                return false;
            }

            users.Add(user);
            SaveUsers(users);
            return true;
        }

        public static bool ValidateLogin(string email, string password, out User? foundUser)
        {
            foundUser = null;
            var users = LoadUsers();

            foundUser = users.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            return foundUser != null;
        }
    }
}