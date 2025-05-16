using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly TechXpressDBContext _context;

        public DiscountRepo(TechXpressDBContext context)
        {
            _context = context;
        }

        public Discount GetByCode(string code)
        {
            return _context.discounts
                .FirstOrDefault(d => d.Code == code && d.IsActive &&
                                     (d.ExpiryDate == null || d.ExpiryDate > DateTime.Now));
        }

        public void Add(Discount discount)
        {
            _context.discounts.Add(discount);
            _context.SaveChanges();
        }

        public IQueryable<Discount> GetAll()
        {
            return _context.discounts;
        }
    }

}
