﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Review : baseEntity
    {
        
        //public int? Id { get; set; }
        public string? ReviewDescription { get; set; }
        public int? Rating { get; set; } //on a scale of 1 to 5 with 1 'star' being the worst and 5 'stars' being the best
        
        public string UserID { get; set; }
        public ApplicationUser? User { get; set; }

        public int? ProductID { get; set; }
        public Product? Product { get; set; }
        

    }
}
