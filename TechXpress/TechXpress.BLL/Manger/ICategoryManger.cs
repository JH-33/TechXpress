using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO.AccountDto;

namespace TechXpress.BLL.Manger
{
    public interface ICategoryManger
    {
        IEnumerable<CategoryDto> GetAll();
        CategoryDto GetById(int id);

        void Insert(CategoryDto categoryDto);
        void Update(CategoryDto categoryDto);
        void Delete(int id);
        void SaveChanges();
    }
}
