using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
       
        public string? Address { get; set; }

        public ICollection<Order>? Orders { get; set; } //1-to-mSS
        public ICollection<Product>? Products { get; set; } //m-to-m
        public ICollection<Review>? Reviews { get; set; } //1-to-m
        public int? ShoppingCart_ID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; } //1-to-1
        

    }
}
