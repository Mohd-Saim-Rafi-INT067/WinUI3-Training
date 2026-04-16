using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty; //Uniqure Id for each product
        public decimal Price { get; set; }
        public decimal DiscountPct { get; set; }
        public decimal FinalPrice { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
