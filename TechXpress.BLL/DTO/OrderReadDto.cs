using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO
{
    public class OrderReadDto
    {
        
            public int OrderID { get; set; }
            public DateTime? OrderDate { get; set; }
            public int? TotalAmountToPay { get; set; }
            public string? Order_Status { get; set; }
            public string? Shipping_Address { get; set; }
            public string ?UserID { get; set; } 
            //public int? PaymentID { get; set; }
            public int? ShoppingCart_ID { get; set; }
        

    }
}
