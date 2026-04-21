using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment_Product_Manager.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }


        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 200 characters")]
        public string ProductName { get; set; } = string.Empty;


        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; } = string.Empty;


        public string Category { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = " Price must be greater than 0")]
        public decimal Price { get; set; }
        public decimal DiscountPct { get; set; }
        public decimal FinalPrice { get; set; }

        [Range(0, int.MaxValue ,ErrorMessage ="Stock cannot be negative")]
        public int Stock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = " Quantity cannot be negative")]
        public int Quantity { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
