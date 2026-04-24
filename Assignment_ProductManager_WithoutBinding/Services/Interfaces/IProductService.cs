using Assignment_ProductManager_WithoutBinding.DTOs;
using Assignment_ProductManager_WithoutBinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetMyProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<ServiceResultDto> AddProductAsync(Product product);
        Task<ServiceResultDto> UpdateProductAsync(Product product);
        Task<ServiceResultDto> DeleteProductAsync(int productId);
    }
}
