using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO
{
    public class ShoppingCartReadDto
    {
        public int ShoppingCart_ID { get; set; }
        public int NumberofItems { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UserID { get; set; }
        public ICollection<ProductReadDto> Products { get; set; } 
    }

}
