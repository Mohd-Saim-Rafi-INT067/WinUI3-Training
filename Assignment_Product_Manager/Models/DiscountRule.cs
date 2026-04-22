using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.Models
{
    public class DiscountRule
    {
        public int RuleId { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public string RuleLabel { get; set; } = string.Empty;
    }
}
