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

            var order = productManger.GetById(Id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Insert(ProductAddDto productAdd)
        {
            productManger.Insert(productAdd);
            return NoContent();
        }
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, ProductUpdateDto productUpdate)
        {
            if (Id != productUpdate.ProductId)
                return BadRequest();

            productManger.Update(productUpdate);
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            productManger?.Delete(Id);
            return NoContent();
        }
    }
}
