using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    
        public class PaymentAddDto
        {
            public string PaymentType { get; set; }
            public int PaymentAmount { get; set; }
            public string PaymentDate { get; set; }
            public int? OrderID { get; set; }  
        }

    
}
