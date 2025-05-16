using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public interface IDiscountManager
    {
        Discount GetValidDiscount(string code);
        void CreateDiscount(Discount discount);
        IEnumerable<Discount> GetAll();
    }
}
