﻿using Microsoft.AspNetCore.Authorization;
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

            var payment = paymentManger.GetById(Id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public ActionResult Insert(PaymentAddDto paymentAdd)
        {
            paymentManger.Insert(paymentAdd);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public ActionResult Update(int Id, PaymentUpdateDto paymentUpdate)
        {
            if (Id != paymentUpdate.PaymentID)
                return BadRequest();

            paymentManger.Update(paymentUpdate);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            paymentManger?.Delete(Id);
            return NoContent();
        }
    }
}
