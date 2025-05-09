using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class baseEntity
    {
        
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }


        public DateTime DeletedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public String DeletedBy { get; set; }


    }
}