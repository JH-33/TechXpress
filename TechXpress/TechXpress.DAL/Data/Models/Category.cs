using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.DAL.Data.Models
{
    public class Category : baseEntity
    {
                
       // public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
