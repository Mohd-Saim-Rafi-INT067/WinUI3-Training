using Assignment_ProductManager_WithoutBinding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.Core.Validators
{
    public static class ProductInputValidator
    {
        public static string? Validate(
            string priceText,
            string stockText,
            string quantityText,
            out decimal price,
            out int stock,
            out int qty)
        {
            price = 0;
            stock = 0;
            qty = 0;

            if (!ValidationHelper.IsValidPrice(priceText, out price))
                return "Enter a valid price greater than 0.";

            if (!ValidationHelper.IsValidStock(stockText, out stock))
                return "Enter a valid stock quantity (0 or more).";

            if (!int.TryParse(quantityText, out qty) || qty < 0)
                return "Enter a valid quantity (0 or more).";

            return null; 
        }
    }
}
