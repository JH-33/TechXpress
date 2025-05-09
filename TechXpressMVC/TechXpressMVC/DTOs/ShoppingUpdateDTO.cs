using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpressMVC.DTOs
{
    public class ShoppingCartUpdateDto
    {
        public int ShoppingCart_ID { get; set; }
        public int? NumberofItems { get; set; }
    }

}
