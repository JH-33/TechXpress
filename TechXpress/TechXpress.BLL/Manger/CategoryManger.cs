using TechXpress.BLL.DTO.AccountDto;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace TechXpress.BLL.Manger
{
    public class CategoryManger : ICategoryManger
    {
        private readonly ICategoryrepo categoryrepo;
        private readonly IMemoryCache _CategoryCache;
        private const string _OurCache = "Category_Cache";

        public CategoryManger(ICategoryrepo _categoryrepo, IMemoryCache CatCache)
        {
            categoryrepo = _categoryrepo;
            _CategoryCache = CatCache;
        }
        public void Delete( int id)
        {
            var model = categoryrepo.GetById(id);
            categoryrepo.Delete(model);
            SaveChanges();

        }

        public IEnumerable<CategoryDto> GetAll()
        {
            if(!_CategoryCache.TryGetValue(_OurCache, out IEnumerable<CategoryDto> categoryDto))
            { 
                var model1 = categoryrepo.GetAll();
                var categorydto = model1.Select(a => new CategoryDto
                {
                    Id = a.Id,
                    CategoryName = a.CategoryName,
                }).ToList();

                MemoryCacheEntryOptions cachingOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };
            }
           
            return categoryDto;
        }

        public CategoryDto GetById(int id)
        {
            if (!_CategoryCache.TryGetValue(_OurCache, out CategoryDto categoryDto))
            {
                     var model2 = categoryrepo.GetById(id);

                     categoryDto = new CategoryDto
                    {
                        Id = model2.Id,
                        CategoryName=model2.CategoryName,
                    };

                    MemoryCacheEntryOptions cachingOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                    };

                    _CategoryCache.Set($"{_OurCache}_{id}", categoryDto);
            }
             
            return categoryDto;
        }

        public CategoryDto GetByName(string name)
        {
            var model2 = categoryrepo.GetByName(name);
            CategoryDto categoryDto = new CategoryDto()
            {
                CategoryName = model2.CategoryName,
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
