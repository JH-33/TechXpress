using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public interface IProductManger
    {
        IEnumerable<ProductReadDto> GetAll();
        ProductReadDto GetById(int id);

        void Insert(ProductAddDto productAdd);
        void Update(ProductUpdateDto productUpdate);
        void Delete(int id);
    }
}
