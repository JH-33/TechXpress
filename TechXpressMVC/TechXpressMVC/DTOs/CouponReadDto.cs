using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    public class CouponReadDto
    {


        public string? Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        //public bool Active { get; set; }
        public int? UsageLimit { get; set; }
        public int? UsageCount { get; set; }
        public int? couponDiscountinPercentage { get; set; }


    }
}
