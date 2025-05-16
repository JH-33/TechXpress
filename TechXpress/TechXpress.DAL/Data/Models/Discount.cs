using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    
        public class Discount:baseEntity
        {
            //public int Id { get; set; }
            public string Code { get; set; } // مثال: SAVE10
            public decimal Percentage { get; set; } // مثال: 10
            public bool IsActive { get; set; } = true;
            public DateTime? ExpiryDate { get; set; }
        }
    
}
