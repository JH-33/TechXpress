using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManger _categoryManger;

        public CategoryController(ICategoryManger categoryManger)
        {
            _categoryManger = categoryManger;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_categoryManger.GetAll());
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            var category = _categoryManger.GetById(Id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("ByName/{name}")]
        public ActionResult GetByName(string name)
        {
            var category = _categoryManger.GetByName(name);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Insert(CategoryDto categoryDto)
        {
            _categoryManger.Insert(categoryDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, CategoryDto categoryDto)
        {
            if (Id != categoryDto.Id)
                return BadRequest("ID mismatch.");

            _categoryManger.Update(categoryDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            _categoryManger.Delete(Id);
            return NoContent();
        }
    }
}
