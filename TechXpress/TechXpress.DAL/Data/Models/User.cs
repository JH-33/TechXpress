using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Data.Models
{
    public class User
    {
        [Key]public int? UserID { get; set; }
        public string? UserName { get; set; }
        public int? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; } //admin or normal user

        public ICollection<Order>? Orders { get; set; } //1-to-m
        public ICollection<Product>? Products { get; set; } //m-to-m
        public ICollection<Review>? Reviews { get; set; } //1-to-m
        public ShoppingCart? ShoppingCart { get; set; } //1-to-1
        public int? ShoppingCart_ID { get; set; }

    }
}
