using Assignment_ProductManager_WithoutBinding.DTOs;
using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.Repositories.Interfaces;
using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.Services
{
    public class ProductService : IProductService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;

        public ProductService(IServiceProvider serviceProvider, IAuthService authService)
        {
            _serviceProvider = serviceProvider;
            _authService = authService;
        }

        private int CurrentUserId => _authService.CurrentUser?.UserId
            ?? throw new InvalidOperationException("No user is logged in.");

        public async Task<IEnumerable<Product>> GetMyProductsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                return await repo.GetProductsByUserAsync(CurrentUserId);
            } 
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                return await repo.GetProductByIdAsync(productId);
            } 
        }

        public async Task<ServiceResultDto> AddProductAsync(Product product)
        {
            product.UserId = CurrentUserId;
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var ok = await repo.AddProductAsync(product);
                return ok
                    ? ServiceResultDto.Ok("Product added successfully.")
                    : ServiceResultDto.Fail("Failed to add product.");
            } 
        }

        public async Task<ServiceResultDto> UpdateProductAsync(Product product)
        {
            product.UserId = CurrentUserId;
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var ok = await repo.UpdateProductAsync(product);
                return ok
                    ? ServiceResultDto.Ok("Product updated successfully.")
                    : ServiceResultDto.Fail("Failed to update product.");
            } 
        }

        public async Task<ServiceResultDto> DeleteProductAsync(int productId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var ok = await repo.DeleteProductAsync(productId, CurrentUserId);
                return ok
                    ? ServiceResultDto.Ok("Product deleted.")
                    : ServiceResultDto.Fail("Failed to delete product.");
            } 
        }
    }
}