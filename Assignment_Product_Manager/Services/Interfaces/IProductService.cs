using Assignment_Product_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetMyProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<(bool Success, string Message)> AddProductAsync(Product product);
        Task<(bool Success, string Message)> UpdateProductAsync(Product product);
        Task<(bool Success, string Message)> DeleteProductAsync(int productId);
    }
}
