using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManger categoryManger;

        public CategoryController(ICategoryManger _categoryManger)
        {
            categoryManger = _categoryManger;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(categoryManger.GetAll());
        }
        [HttpGet("GetBy{Id}")]
        public ActionResult GetById(int Id)
        {
            return Ok(categoryManger.GetById(Id));
        }

        [HttpPost]
        public ActionResult Insert(CategoryDto categoryDto)
        {
            categoryManger.Insert(categoryDto);
            return NoContent();
        }

        [HttpGet("GetName{Name}")]
        //public ActionResult GetByName(string Name)
        //{

        //    //var product = CategoryManger.GetByName(a =>  a.Name);
        //    //if (product == null)
        //    //    return NotFound();
        //    //return Ok(product);
        //}
  

        [HttpPut("{Id}")]
        public ActionResult Update(int Id, CategoryDto categoryDto)
        {
            if (Id != categoryDto.Id)
                return BadRequest();

            categoryManger.Update(categoryDto);
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            categoryManger?.Delete(Id);
            return NoContent();
        }
    }
}
