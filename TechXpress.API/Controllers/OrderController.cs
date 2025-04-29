using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;
using TechXpress.DAL.Data.Models;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManger orderManger;
        private readonly ICouponManager couponManager;

        public OrderController(IOrderManger _orderManger , ICouponManager _couponManager)
        {
            orderManger = _orderManger;
            couponManager = _couponManager;
        }
        [HttpGet]
        public ActionResult GetAll()
        { var res= orderManger.GetAll();
            if (res == null)
                return NotFound();
            return Ok(res);
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {

            var order = orderManger.GetById(Id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Insert(OrderAddDto orderAddDto)
        {
            orderManger.Insert(orderAddDto);
            return NoContent();
        }
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, OrderUpdateDto orderUpdateDto)
        {
            if (Id != orderUpdateDto.OrderID)
                return BadRequest();

            orderManger.Update(orderUpdateDto);
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            orderManger?.Delete(Id);
            return NoContent();
        }



        [HttpPut("ApplyDiscount/{Coupon}")]
        public ActionResult ApplyDiscountToOrder(string Coupon, OrderUpdateDto orderUpdateDto)
        {
           
            var ValidCoupon = couponManager.GetByCode(Coupon);
            if(ValidCoupon == null)
            {
                return NotFound();
            }
            orderManger.ApplyDiscountToOrder(ValidCoupon.Code, orderUpdateDto);
            return NoContent();
            }

           
        
    }
}
