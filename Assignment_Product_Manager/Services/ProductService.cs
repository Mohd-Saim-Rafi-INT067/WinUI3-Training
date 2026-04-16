using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.Repositories.Interfaces;
using Assignment_Product_Manager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IAuthService _authService;

        public ProductService(IProductRepository productRepo, IAuthService authService)
        {
            _productRepo = productRepo;
            _authService = authService;
        }

        private int CurrentUserId => _authService.CurrentUser?.UserId
            ?? throw new InvalidOperationException("No user is logged in.");

        public Task<IEnumerable<Product>> GetMyProductsAsync()
            => _productRepo.GetProductsByUserAsync(CurrentUserId);

        public Task<Product?> GetProductByIdAsync(int productId)
            => _productRepo.GetProductByIdAsync(productId);

        public async Task<(bool Success, string Message)> AddProductAsync(Product product)
        {
            product.UserId = CurrentUserId;
            var ok = await _productRepo.AddProductAsync(product);
            return ok ? (true, "Product added successfully.") : (false, "Failed to add product.");
        }

        public async Task<(bool Success, string Message)> UpdateProductAsync(Product product)
        {
            product.UserId = CurrentUserId;
            var ok = await _productRepo.UpdateProductAsync(product);
            return ok ? (true, "Product updated successfully.") : (false, "Failed to update product.");
        }

        public async Task<(bool Success, string Message)> DeleteProductAsync(int productId)
        {
            var ok = await _productRepo.DeleteProductAsync(productId, CurrentUserId);
            return ok ? (true, "Product deleted.") : (false, "Failed to delete product.");
        }
    }
}