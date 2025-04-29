using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Migrations;

namespace TechXpress.DAL.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly TechXpressDBContext context;

        public ProductRepo(TechXpressDBContext _context)
        {
            context = _context;
        }
        public void Delete(Product product)
        {
            context.Remove(product);
            SaveChanges();
        }

        public IQueryable<Product> GetAll()
        {
            return context.Products.AsNoTracking();
        }

        public Product GetById(int id)
        {
            return context.Products.Find(id);
        }

        public Product GetByName(string name)
        {
            return context.Products.Find(name);
        }

        public void Insert(Product product)
        {
            context.Add(product);
            SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Update(product);
        }
    }
}
