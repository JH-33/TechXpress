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
        [HttpGet("Product")]
        public ActionResult GetAllProduct()
        {
            return Ok(productManger.GetAll());
        }
        [HttpGet("Product/{Id}")]
        public ActionResult GetById(int Id)
        {

            var product = productManger.GetById(Id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public ActionResult AddProduct(ProductAddDto productAdd)
        {
            productManger.Insert(productAdd);
            return NoContent();
        }
        [HttpPut("UpdateProduct/{Id}")]
        public ActionResult UpdateProduct(int Id, ProductUpdateDto productUpdate)
        {
            if (Id != productUpdate.ProductId)
                return BadRequest();

            productManger.Update(productUpdate);
            return NoContent();
        }
        [HttpDelete("DeleteProduct{Id}")]
        public ActionResult Delete(int Id)
        {
            productManger?.Delete(Id);
            return NoContent();
        }
    }
}
