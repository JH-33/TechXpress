using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Migrations;

namespace TechXpress.DAL.Repository
{
    public interface ICategoryrepo
    {
        IQueryable<Category> GetAll();
        Category GetById(int id);
        //Category GetByName(string name);
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);
        void SaveChanges();
    }
}
