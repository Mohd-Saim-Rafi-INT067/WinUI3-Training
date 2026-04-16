using Assignment_Product_Manager.Core.Helpers;
using Assignment_Product_Manager.Repositories.Interfaces;
using Assignment_Product_Manager.Services.Interfaces;
using System.Threading.Tasks;
using Assignment_Product_Manager.Models;

namespace Assignment_Product_Manager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        public User? CurrentUser { get; private set; }
        //can either be a object or be null here
        public bool IsLoggedIn => CurrentUser != null;

        public AuthService(IUserRepository userRepo) => _userRepo = userRepo;

        public async Task<(bool Success, string Message, User? User)> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return (false, "Username is required.", null);
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password is required.", null);

            var hash = ValidationHelper.HashPassword(password);
            var user = await _userRepo.LoginAsync(username.Trim(), hash);

            if (user == null)
                return (false, "Invalid username or password.", null);
            if (!user.IsActive)
                return (false, "Your account is deactivated. Contact support.", null);

            CurrentUser = user;
            return (true, "Login successful.", user);
        }

        public async Task<(bool Success, string Message)> RegisterAsync(
            string username, string password, string email, string fullName, string phone)
        {

            if (!ValidationHelper.IsValidUsername(username))
                return (false, "Username must be 3–30 characters (letters, numbers, underscore only).");
            if (!ValidationHelper.IsValidPassword(password))
                return (false, "Password must be at least 6 characters.");
            if (!ValidationHelper.IsValidEmail(email))
                return (false, "Enter a valid email address.");
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "Full name is required.");

            if (await _userRepo.UsernameExistsAsync(username.Trim()))
                return (false, "Username is already taken.");
            if (await _userRepo.EmailExistsAsync(email.Trim()))
                return (false, "Email is already registered.");

            var user = new User
            {
                Username = username.Trim(),
                Password = ValidationHelper.HashPassword(password),
                Email = email.Trim(),
                FullName = fullName.Trim(),
                PhoneNumber = phone.Trim(),
            };

            var ok = await _userRepo.RegisterAsync(user);
            return ok
                ? (true, "Account created successfully! Please log in.")
                : (false, "Registration failed. Please try again.");
        }

        public void Logout() => CurrentUser = null;
    }
}
