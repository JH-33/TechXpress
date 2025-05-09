using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; } 

        [Required]
        public string CategoryName { get; set; }
    }
}
