using System.Reflection.Metadata.Ecma335;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class ProductManger : IProductManger
    {
        private readonly IProductRepo productRepo;

        public ProductManger(IProductRepo _productRepo)
        {
            productRepo = _productRepo;
        }
        public void Delete(int id)
        {
            var modelDelete = productRepo.GetById(id);
            if (modelDelete == null) throw new Exception();
            productRepo.Delete(modelDelete);
            productRepo.SaveChanges();
        }

        public IEnumerable<ProductReadDto> GetAll()
        {
            var modelreadfromdatabase = productRepo.GetAll();
            var modelread = modelreadfromdatabase.Select(a => new ProductReadDto
            {
              ProductId = a.ProductId,
              ProductName=a.ProductName,
              ProductDescription=a.ProductDescription,
              Price=a.Price,
              StockQuantity=a.StockQuantity,
              categoryid=a.categoryid
            });
            return modelread;
        }
        
        public ProductReadDto GetById(int id)
        {
            var modelreadfromdatabase = productRepo.GetById(id);
            if (modelreadfromdatabase == null) return null;
            var modeldto = new ProductReadDto()
            {
                ProductId = modelreadfromdatabase.ProductId,
                ProductName=modelreadfromdatabase.ProductName,
                ProductDescription=modelreadfromdatabase.ProductDescription,
                Price=modelreadfromdatabase.Price,
                StockQuantity=modelreadfromdatabase.StockQuantity,
                categoryid=modelreadfromdatabase.categoryid
            };
            return modeldto;
        }

        public void Insert(ProductAddDto productAddDto)
        {
            var model = new Product()
            {
                ProductName = productAddDto.ProductName,
                ProductDescription = productAddDto.ProductDescription,
                Price = productAddDto.Price,
                StockQuantity = productAddDto.StockQuantity,
                categoryid = productAddDto.CategoryId,
            };
            productRepo.Insert(model);
            productRepo.SaveChanges();
        }

        public void Update(ProductUpdateDto productUpdate)
        {
            var model = productRepo.GetById(productUpdate.ProductId);
            if (model == null) throw new Exception();
            model.ProductName = productUpdate.ProductName;
            model.ProductDescription = productUpdate.ProductDescription;
            model.Price = productUpdate.Price;
            model.StockQuantity = productUpdate.StockQuantity;
            model.categoryid = productUpdate.CategoryId;
      

        productRepo.Update(model);
            productRepo.SaveChanges();
        }
    }
}
