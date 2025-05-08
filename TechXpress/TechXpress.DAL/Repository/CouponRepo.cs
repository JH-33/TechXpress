using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class CouponRepo : ICouponRepo
    {
        private readonly TechXpressDBContext context;

        public CouponRepo(TechXpressDBContext _context)
        {
            context = _context;

        }

        public void Delete(Coupon Coupon)
        {
            context.Remove(Coupon);
            SaveChanges();
        }

        public IQueryable<Coupon> GetAllActive()
        {
            var active = context.Coupons.Where(a =>
                a.ExpirationDate.Date >= DateTime.Now.Date &&
                a.UsageLimit >= (a.UsageCount ?? 0));
            return active.AsNoTracking();
        }

        public void Insert(Coupon Coupon)
        {
            context.Add(Coupon);
            SaveChanges();
        }

        public void Update(Coupon Coupon)
        {
            SaveChanges();
        }

        public Coupon GetById(int id)
        {
            return context.Coupons.Find(id);
        }

        public Coupon GetByCode(string code)
        {
            return context.Coupons
                  .AsNoTracking()
                  .FirstOrDefault(c => c.Code == code);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
