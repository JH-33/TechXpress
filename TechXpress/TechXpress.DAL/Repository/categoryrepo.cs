using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class categoryrepo : ICategoryrepo
    {
        private readonly TechXpressDBContext context;

        public categoryrepo(TechXpressDBContext _Context)
        {
            context = _Context;
        }

        public void Delete(Category category)
        {
            context.Remove(category);
        }

        public IQueryable<Category> GetAll()
        {
            return context.Categories.AsNoTracking();
        }

        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }

        public void Insert(Category category)
        {
             context.Add(category);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Category category)
        {
           context.Update(category);
        }
    }
}
