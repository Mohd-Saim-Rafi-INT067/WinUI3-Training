using Assignment_Product_Manager.Core.Constants;
using Assignment_Product_Manager.Core.Database;
using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.Repositories.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnectionFactory _db;

        public ProductRepository(DbConnectionFactory db) => _db = db;

        public async Task<IEnumerable<Product>> GetProductsByUserAsync(int userId)
        {
            var products = new List<Product>();
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.GetProductsByUser, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_UserId", userId);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(MapProduct(reader));
            }
            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.GetProductById, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_ProductId", productId);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
                return MapProduct(reader);
            return null;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.AddProduct, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            AddProductParams(cmd, product);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.UpdateProduct, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_ProductId", product.ProductId);
            AddProductParams(cmd, product);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId, int userId)
        {
            await using var conn = await _db.CreateConnectionAsync();
            await using var cmd = new MySqlCommand(AppConstants.StoredProcedures.DeleteProduct, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("p_ProductId", productId);
            cmd.Parameters.AddWithValue("p_UserId", userId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && Convert.ToInt32(result) > 0;
        }

        private static void AddProductParams(MySqlCommand cmd, Product p)
        {
            cmd.Parameters.AddWithValue("p_UserId", p.UserId);
            cmd.Parameters.AddWithValue("p_ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("p_Description", p.Description);
            cmd.Parameters.AddWithValue("p_Category", p.Category);
            cmd.Parameters.AddWithValue("p_SKU", p.SKU);
            cmd.Parameters.AddWithValue("p_Price", p.Price);
            cmd.Parameters.AddWithValue("p_Stock", p.Stock);
            cmd.Parameters.AddWithValue("p_Quantity", p.Quantity);
            cmd.Parameters.AddWithValue("p_ImageData",
        (object?)p.ImageData ?? DBNull.Value);
        }

        private static Product MapProduct(MySqlDataReader r) => new()
        {
            ProductId = r.GetInt32("ProductId"),
            UserId = r.GetInt32("UserId"),
            ProductName = r.GetString("ProductName"),
            Description = r.IsDBNull(r.GetOrdinal("Description")) ? "" : r.GetString("Description"),
            Category = r.IsDBNull(r.GetOrdinal("Category")) ? "" : r.GetString("Category"),
            SKU = r.IsDBNull(r.GetOrdinal("SKU")) ? "" : r.GetString("SKU"),
            Price = r.GetDecimal("Price"),
            DiscountPct = r.GetDecimal("DiscountPct"),
            FinalPrice = r.GetDecimal("FinalPrice"),
            Stock = r.GetInt32("Stock"),
            Quantity = r.GetInt32("Quantity"),
            IsActive = r.GetBoolean("IsActive"),
            CreatedAt = r.GetDateTime("CreatedAt"),
            UpdatedAt = r.GetDateTime("UpdatedAt"),
            ImageData = r.IsDBNull(r.GetOrdinal("ImageData"))
                    ? null
                    : (byte[])r["ImageData"],
        };
    }
}
