using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    public class ProductReadDto
    {
        public int ProductId { get; set; }            
        public string ProductName { get; set; }       
        public string ProductDescription { get; set; } 
        public int ?Price { get; set; }                
        public int? StockQuantity { get; set; }       
        public int ?categoryid { get; set; }           
    }

}
