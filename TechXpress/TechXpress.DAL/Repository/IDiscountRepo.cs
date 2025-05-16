using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    
        public interface IDiscountRepo
        {
            Discount GetByCode(string code);
            void Add(Discount discount);
        IQueryable<Discount> GetAll();
        }
    
}
