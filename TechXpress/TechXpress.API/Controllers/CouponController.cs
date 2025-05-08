using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.Manger;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponManager couponManager;
        public CouponController(ICouponManager _couponManager)
        {
            couponManager = _couponManager;
        }
        [HttpGet]
        public ActionResult GetAllActive()
        {
            var res = couponManager.GetAllActive();
            if (res == null)
            {
                return NotFound("No active coupons found.");
            }
            return Ok(res);
        }

        [HttpGet("{ID}")]

        public ActionResult GetByCode(string code)
        {
            var coupon = couponManager.GetByCode(code);
            if (coupon == null)
            {
                return NotFound("Coupon not found.");
            }
            return Ok(coupon);
        }
        [HttpPut("{code}")]
        public ActionResult UpdateCoupon(string code, CouponUpdateDto couponUpdateDto)
        {
            if (code != couponUpdateDto.Code)
            {
                return BadRequest("Coupon code mismatch.");
            }
            var existingCoupon = couponManager.GetByCode(code);
            if (existingCoupon == null)
            {
                return NotFound("Coupon not found.");
            }
            couponManager.UpdateCoupon(couponUpdateDto);
            return NoContent();
        }

        [HttpPost]
        public ActionResult AddCoupon(CouponAddDto couponAddDto)
        {
            couponManager.AddCoupon(couponAddDto);

            return NoContent();
        }

        [HttpDelete("{code}")]
        public ActionResult DeleteCoupon(int code)
        {
            var res = couponManager.GetByID(code);
            if (res == null)
            {
                return NotFound("Coupon not found.");
            }

            couponManager.DeleteCoupon(code);
            return NoContent();
        }
    }
}
