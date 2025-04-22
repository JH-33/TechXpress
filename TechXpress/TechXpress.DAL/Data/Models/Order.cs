using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int? TotalAmountToPay { get; set; }
        public string? Order_Status { get; set; }
        public string? Shipping_Address { get; set; }
        public string ?UserID { get; set; }
        public ApplicationUser User { get; set; }

        public int? PaymentID { get; set; }
        public Payment? Payment { get; set; }

        public int? ShoppingCart_ID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        //public ICollection<Product> products { get; set; }


    } 
}
