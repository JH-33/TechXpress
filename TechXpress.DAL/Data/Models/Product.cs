using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int? StockQuantity { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? Price { get; set; }

        public ICollection<ApplicationUser>? User { get; set; }
        public ICollection<Review>? Reviews { get; set; }

        public int? catgoryid { get; set; }
        public Category? Category { get; set; }
        

        
        public int? Cart_ID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
    

}
