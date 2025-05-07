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
            return context.Categories;
        }

        public Category GetById(int id)
        {
            return context.Categories.Find(id);
        }

        //public Category GetByName(string name)
        //{
        //    return context.Categories.Find(name);
        //}

        public void Insert(Category category)
        {
             context.Add(category);
        }

        public void Update(Category category)
        {

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        
    }
}
