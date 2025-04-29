using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;
using TechXpress.DAL.Data.Models;

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
        [HttpGet("checkOut/{cartid}")]
        public ActionResult CheckOut(int cartid)
        {
            return Ok(shoppingManger.Checkout(cartid));
        }


        [HttpPost("CreateShoppingCart")]
        public ActionResult CreateShoppingCart(ShoppingAddDto shoppingAddDto)
        {
            shoppingManger.CreateShoppingCart(shoppingAddDto);
            return NoContent();
        }
        [HttpPost("AddProduct/{cartId}/{ProductId}")]
        public ActionResult AddProductToCart(int cartId,int ProductId,ShoppingAddDto shoppingAddDto)
        {
            shoppingManger.AddProductToCart(cartId,ProductId,shoppingAddDto);
            return NoContent();
        }
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
            shoppingManger.RemoveProductFromCart(CartId,ProductId);
            return NoContent();
        }

    }
}
