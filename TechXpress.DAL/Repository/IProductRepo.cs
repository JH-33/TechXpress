using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface IProductRepo
    {
        IQueryable<Product> GetAll();
        Product GetById(int id);

        void Insert(Product product);
        void Update(Product product);
        void Delete(Product product);
        void SaveChanges();
    }
}
