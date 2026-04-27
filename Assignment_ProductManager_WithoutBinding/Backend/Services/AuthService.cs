using Assignment_ProductManager_WithoutBinding.Core.Helpers;
using Assignment_ProductManager_WithoutBinding.DTOs;
using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.Repositories.Interfaces;
using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Assignment_ProductManager_WithoutBinding.Core.Validators;

namespace Assignment_ProductManager_WithoutBinding.Services
{
    public class AuthService : IAuthService
    {
        //builtin DI container in .net
        private readonly IServiceProvider _serviceProvider;
        public User? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public AuthService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<AuthResultDto> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return AuthResultDto.Fail("Username is required.");
            if (string.IsNullOrWhiteSpace(password))
                return AuthResultDto.Fail("Password is required.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var hash = ValidationHelper.HashPassword(password);
                var user = await userRepo.LoginAsync(username.Trim(), hash);

                if (user == null)
                    return AuthResultDto.Fail("Invalid username or password.");
                if (!user.IsActive)
                    return AuthResultDto.Fail("Your account is deactivated. Contact support.");

                CurrentUser = user;
                return AuthResultDto.Ok("Login successful.", user);
            } 
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterRequest request)
        {
            var error = RegisterRequestValidator.Validate(request);
            if (error != null)
            {
                return AuthResultDto.Fail(error);
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                if (await userRepo.UsernameExistsAsync(request.Username.Trim()))
                    return AuthResultDto.Fail("Username is already taken.");
                if (await userRepo.EmailExistsAsync(request.Email.Trim()))
                    return AuthResultDto.Fail("Email is already registered.");

                var user = new User
                {
                    Username = request.Username.Trim(),
                    Password = ValidationHelper.HashPassword(request.Password),
                    Email = request.Email.Trim(),
                    FullName = request.FullName.Trim(),
                    PhoneNumber = request.PhoneNumber.Trim(),
                };

                var ok = await userRepo.RegisterAsync(user);
                return ok
                    ? AuthResultDto.Ok("Account created successfully! Please log in.", user)
                    : AuthResultDto.Fail("Registration failed. Please try again.");
            } 
        }

        public void Logout() => CurrentUser = null;
    }
}
