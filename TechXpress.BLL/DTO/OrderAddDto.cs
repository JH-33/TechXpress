using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO
{
    public class OrderAddDto
    {
            public string? Shipping_Address { get; set; }
            public string ?UserID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Order_Status { get; set; }
        public int? TotalAmountToPay { get; set; }
        public int? ShoppingCart_ID { get; set; }
        }

    }

