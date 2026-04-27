using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Assignment_ProductManager_WithoutBinding.Models
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

        public byte[]? ImageData { get; set;}

        private WeakReference<BitmapImage>? _cachedBitmap;

        public BitmapImage? GetOrCreateBitmap()
        {
            if (ImageData == null || ImageData.Length == 0)
                return null;

            if (_cachedBitmap != null && _cachedBitmap.TryGetTarget(out var cached))
                return cached; 

            var bitmap = new BitmapImage();
            using var ms = new System.IO.MemoryStream(ImageData);
            using var ras = ms.AsRandomAccessStream();
            bitmap.SetSource(ras);

            _cachedBitmap = new WeakReference<BitmapImage>(bitmap);
            return bitmap;
        }
    }
}
