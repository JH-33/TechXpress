using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public interface IOrderManger
    {
        IEnumerable<OrderReadDto> GetAll();
        OrderReadDto GetById(int id);

        void Insert(OrderAddDto orderAddDto);
        void Update(OrderUpdateDto orderUpdateDto);
        void Delete(int id);
        void SaveChanges();
    }
}
