using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.DTO
{
    public class ReviewReadDto
    {
        [Key]
        public int? ReviewId { get; set; }
        public string? ReviewDescription { get; set; }
        public int? Rating { get; set; } //on a scale of 1 to 5 with 1 'star' being the worst and 5 'stars' being the best
        public string UserID { get; set; }

        public int? ProductID { get; set; }
        
    }
}
