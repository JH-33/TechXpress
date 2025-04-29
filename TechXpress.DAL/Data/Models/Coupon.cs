using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Coupon
    {
        [Key] 
        public string? Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? UsageLimit { get; set; }
        public int? UsageCount { get; set; }

        
        public int? couponDiscountinPercentage { get; set; }

        public ICollection<ShoppingCart>? carts { get; set; }   //optional
    }
}
