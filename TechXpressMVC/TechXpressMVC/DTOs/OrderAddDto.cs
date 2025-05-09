using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    public class OrderAddDto
    {
            public string? Shipping_Address { get; set; }
            public string ?UserID { get; set; }
            public int? PaymentID { get; set; }
            public int? ShoppingCart_ID { get; set; }
        }

    }

