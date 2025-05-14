using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManger orderManger;

        public OrderController(IOrderManger _orderManger)
        {
            orderManger = _orderManger;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(orderManger.GetAll());
        }

        [Authorize]
        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            var order = orderManger.GetById(Id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Insert(OrderAddDto orderAddDto)
        {
            var userId = User.FindFirstValue("uid");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not found.");

            orderAddDto.UserID = userId;
            orderManger.Insert(orderAddDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, OrderUpdateDto orderUpdateDto)
        {
            if (Id != orderUpdateDto.OrderID)
                return BadRequest();

            orderManger.Update(orderUpdateDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            orderManger?.Delete(Id);
            return NoContent();
        }
    }
}
