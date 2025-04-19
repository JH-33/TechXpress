using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public interface IpaymentManger
    {
        PaymentReadDto GetById(int id);

        void Insert( PaymentAddDto paymentAdd);
        void Update(PaymentUpdateDto paymentUpdate);
        void Delete(int id);
        void SaveChanges();
    }
}
