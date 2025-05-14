using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManger productManger;

        public ProductController(IProductManger _productManger)
        {
            productManger = _productManger;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(productManger.GetAll());
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            var product = productManger.GetById(Id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("ByName/{Name}")]
        public ActionResult GetByName(string Name)
        {
            var product = productManger.GetByName(Name);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetBestProduct")]
        public ProductAddDto GetFastSellingProduct(List<ProductAddDto> product)
        {
            return product.OrderBy(p => p.StockQuantity).FirstOrDefault();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddProduct(ProductAddDto productAdd)
        {
            productManger.Insert(productAdd);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public ActionResult UpdateProduct(int Id, ProductUpdateDto productUpdate)
        {
            if (Id != productUpdate.ProductId)
                return BadRequest();

            productManger.Update(productUpdate);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            productManger?.Delete(Id);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateStock")]
        public IActionResult UpdateStock(int productId, int quantity, bool isIncrease)
        {
            try
            {
                var remaining = productManger.UpdateStockQuantity(productId, quantity, isIncrease);
                return Ok(new { message = $"Inventory updated. Remaining: {remaining}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
