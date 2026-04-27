using Assignment_ProductManager_WithoutBinding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsByUserAsync(int userId);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<bool> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId, int userId);
    }
}
