using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly IShoppingManager shoppingManger;

        public ShoppingController(IShoppingManager _shoppingManger)
        {
            shoppingManger = _shoppingManger;
        }

        private string GetUserIdFromToken()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
        }

        [HttpGet("GetAllshoppingcart")]
        public ActionResult GetAllshoppingcart()
        {
            return Ok(shoppingManger.GetAllShoppingCarts());
        }

        [HttpGet("GetById/{Id}")]
        public ActionResult GetById(int Id)
        {
            var cart = shoppingManger.GetById(Id);
            return Ok(cart);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("checkOut/{cartid}")]
        public ActionResult CheckOut(int cartid)
        {
            var userId = GetUserIdFromToken();
            return Ok(shoppingManger.Checkout(cartid, userId));
        }

        [Authorize(Roles = "customer")]
        [HttpPost("CreateShoppingCart")]
        public ActionResult CreateShoppingCart(ShoppingAddDto shoppingAddDto)
        {
            shoppingAddDto.UserID = GetUserIdFromToken();
            shoppingManger.CreateShoppingCart(shoppingAddDto);
            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpPost("AddProduct/{cartId}/{ProductId}")]
        public ActionResult AddProductToCart(int cartId, int ProductId, ShoppingAddDto shoppingAddDto)
        {
            shoppingAddDto.UserID = GetUserIdFromToken();
            shoppingManger.AddProductToCart(cartId, ProductId, shoppingAddDto);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateShopping/{Id}")]
        public ActionResult UpdateShopping(int Id, ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            if (Id != shoppingCartUpdateDto.ShoppingCart_ID)
                return BadRequest();

            shoppingManger.Update(shoppingCartUpdateDto);
            return NoContent();
        }

        [HttpDelete("RemoveShoppingCart/{Id}")]
        public ActionResult RemoveShoppingCart(int Id)
        {
            shoppingManger?.RemoveShoppingCart(Id);
            return NoContent();
        }

        [HttpDelete("RemoveProduct/{CartId}/{ProductId}")]
        public ActionResult RemoveProduct(int CartId, int ProductId)
        {
            shoppingManger.RemoveProductFromCart(CartId, ProductId);
            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpPost("{cartId}/apply-discount")]
        public IActionResult ApplyDiscount(int cartId, [FromBody] string discountCode)
        {
            try
            {
                var userId = GetUserIdFromToken();
                shoppingManger.ApplyDiscount(cartId, discountCode, userId);
                return Ok("Discount code has been added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
