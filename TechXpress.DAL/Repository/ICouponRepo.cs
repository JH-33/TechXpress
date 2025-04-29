using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface ICouponRepo
    {
         IQueryable<Coupon> GetAllActive();
        void Insert(Coupon Coupon);
        void Update(Coupon Coupon);
        void Delete(Coupon Coupon);
        Coupon GetById(int id);
        Coupon GetByCode(string code);
        void SaveChanges();
        
    }
}
