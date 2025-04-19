using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;
using TechXpress.DAL.Data.Models;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IpaymentManger paymentManger;

        public PaymentController(IpaymentManger _paymentManger)
        {
            paymentManger = _paymentManger;
        }
        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {

            var order = paymentManger.GetById(Id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Insert(PaymentAddDto paymentAdd)
        {
            paymentManger.Insert(paymentAdd);
            return NoContent();
        }
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, PaymentUpdateDto paymentUpdate)
        {
            if (Id != paymentUpdate.PaymentID)
                return BadRequest();

            paymentManger.Update(paymentUpdate);
            return NoContent();
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            paymentManger?.Delete(Id);
            return NoContent();
        }
    }
}
