using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface IPaymentRepo
    {
        Payment GetById(int id);

        void Insert(Payment payment);
        void Update(Payment payment);
        void Delete(Payment payment);
        void SaveChanges();
    }
}
