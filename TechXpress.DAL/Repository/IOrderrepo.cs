using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface IOrderrepo
    {
        IQueryable<Order> GetAll();
        Order GetById(int id);

        void Insert( Order order);
        void Update(Order order);
        void Delete(Order order);
        void SaveChanges();
    }
}
