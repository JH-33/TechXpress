﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }            
        public string ProductName { get; set; }       
        public string ProductDescription { get; set; } 
        public decimal ?Price { get; set; }                
        public int? StockQuantity { get; set; }       
        public int ?categoryid { get; set; }           
    }

}
