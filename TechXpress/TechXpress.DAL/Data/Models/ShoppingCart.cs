using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechXpress.DAL.Data.Models
{
    public class ShoppingCart : baseEntity
    {
        //public int Id { get; set; }
        public int NumberofItems { get; set; }
        public DateTime? CreatedDate { get; set; }= DateTime.UtcNow;
        public decimal TotalAmount { get; set; } 
        public string? UserID { get; set; }
        public ApplicationUser? User { get; set; }

        public int? Order_ID { get; set; }
        public Order? Order { get; set; }
        
        public ICollection<Product> Products { get; set; }
    }
}
