﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TechXpress.DAL.Data.Models
{
    public class ShoppingCart
    {
        [Key] 
        public int ShoppingCart_ID { get; set; }
        public int NumberofItems { get; set; }
        public string? CreatedDate { get; set; }
        public string? UserID { get; set; }
        public ApplicationUser? User { get; set; }

        public int? Order_ID { get; set; }
        public Order? Order { get; set; }
        

        public ICollection<Product> Products { get; set; }
    }
}
