using TechXpress.BLL.DTO.AccountDto;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class CategoryManger : ICategoryManger
    {
        private readonly ICategoryrepo categoryrepo;

        public CategoryManger(ICategoryrepo _categoryrepo)
        {
            categoryrepo = _categoryrepo;
        }
        public void Delete( int id)
        {
            var model = categoryrepo.GetById(id);
            categoryrepo.Delete(model);
            SaveChanges();
            
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            var model1 = categoryrepo.GetAll();
            var categorydto = model1.Select(a => new CategoryDto
            {
                Id = a.Id,
                CategoryName = a.CategoryName,
            }).ToList();

            return categorydto;
        }

        public CategoryDto GetById(int id)
        {
            var model2 = categoryrepo.GetById(id);
            CategoryDto categoryDto = new CategoryDto()
            {
                Id = model2.Id,
                CategoryName=model2.CategoryName,
            };
            return categoryDto;
        }

        public void Insert(CategoryDto categoryDto)
        {
            var model3 = new Category()
            {
                CategoryName = categoryDto.CategoryName,
                Id = categoryDto.Id
            };
            categoryrepo.Insert(model3);
            SaveChanges();
        }

        public void SaveChanges()
        {
            categoryrepo.SaveChanges();
        }

        public void Update(CategoryDto categoryDto)
        {
            var model4 = categoryrepo.GetById(categoryDto.Id);
            model4.CategoryName = categoryDto.CategoryName;
            model4.Id = categoryDto.Id;
            categoryrepo.Update(model4);
            SaveChanges();

        }
    }
}
