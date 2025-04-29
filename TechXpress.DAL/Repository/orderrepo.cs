using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class orderrepo:IOrderrepo
    {
        private readonly TechXpressDBContext context;

        public orderrepo(TechXpressDBContext _context)
        {
            context = _context;
        }

        public void Delete(Order order)
        {
            context.Remove(order);
        }

        public IQueryable<Order> GetAll()
        {
             return context.Orders.AsNoTracking();
        }

        public Order GetById(int id)
        {
           return context.Orders.Find(id);
        }

        public void Insert(Order order)
        {
            context.Add(order);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Order order)
        {
            
        }
    }
}
