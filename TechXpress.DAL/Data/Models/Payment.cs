using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public string? PaymentType { get; set; }
        public int? PaymentAmount { get; set; }
        public string? PaymentDate { get; set; }
        public int? OrderID { get; set; }
        public Order? Order { get; set; }
        
    }
    

 

}
