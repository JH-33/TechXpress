using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO
{
    public class CouponUpdateDto
    {
        public string? Code { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? UsageLimit { get; set; }

        public int? UsageCount { get; set; }

        public int? couponDiscountinPercentage { get; set; }
    }
}
