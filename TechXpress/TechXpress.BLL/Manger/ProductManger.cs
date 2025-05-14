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
              ProductId = a.Id,
              ProductName=a.ProductName,
              ProductDescription=a.ProductDescription,
              Price=a.Price,
              StockQuantity=a.StockQuantity,
              categoryid=a.categoryid
            }).ToList();
            return modelread;
        }
        
        public ProductReadDto GetById(int id)
        {
            var modelreadfromdatabase = productRepo.GetById(id);
            if (modelreadfromdatabase == null) return null;
            var modeldto = new ProductReadDto()
            {
                ProductId = modelreadfromdatabase.Id,
                ProductName=modelreadfromdatabase.ProductName,
                ProductDescription=modelreadfromdatabase.ProductDescription,
                Price=modelreadfromdatabase.Price,
                StockQuantity=modelreadfromdatabase.StockQuantity,
                categoryid=modelreadfromdatabase.categoryid
            };
            return modeldto;
        }

        public ProductReadDto GetByName(string name)
        {
            var modelreadfromdatabase = productRepo.GetByName(name);
            if (modelreadfromdatabase == null) return null;
            var modeldto = new ProductReadDto()
            {
                ProductId = modelreadfromdatabase.Id,
                ProductName = modelreadfromdatabase.ProductName,
                ProductDescription = modelreadfromdatabase.ProductDescription,
                Price = modelreadfromdatabase.Price,
                StockQuantity = modelreadfromdatabase.StockQuantity,
                categoryid = modelreadfromdatabase.categoryid
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
        public int UpdateStockQuantity(int productId, int quantityChange, bool isIncrease)
        {
            var product = productRepo.GetById(productId);
            if (product == null)
                throw new Exception("Product not found");

            var currentStock = product.StockQuantity ?? 0;

            if (!isIncrease)
            {
                // خصم من المخزون
                if (currentStock < quantityChange)
                    throw new Exception("Quantity not enough");
                currentStock -= quantityChange;
            }
            else
            {
                // إضافة للمخزون
                currentStock += quantityChange;
            }

            product.StockQuantity = currentStock;
            productRepo.Update(product);
            productRepo.SaveChanges();

            return currentStock;
        }

    }
}
