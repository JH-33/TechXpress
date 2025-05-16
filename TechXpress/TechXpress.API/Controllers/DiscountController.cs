using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.Manger;
using TechXpress.DAL.Data.Models;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
            private readonly IDiscountManager _manager;

            public DiscountController(IDiscountManager manager)
            {
                _manager = manager;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                return Ok(_manager.GetAll());
            }
           [Authorize(Roles = "Admin")]

        [HttpPost]
            public IActionResult Create([FromBody] Discount discount)
            {
                _manager.CreateDiscount(discount);
                return Ok("Discount Code has been added successfully");
            }

            [HttpGet("{code}")]
            public IActionResult GetByCode(string code)
            {
                var discount = _manager.GetValidDiscount(code);
                if (discount == null)
                    return NotFound("the discount code is invalid");

                return Ok(discount);
            }
        }

    }

