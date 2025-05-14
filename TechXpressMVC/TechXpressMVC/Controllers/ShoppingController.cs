using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
//using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly HttpClient _httpClient;

        public ShoppingController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ShoppingCartReadDto>>("/api/Shopping/GetAllshoppingcart");
            return View("ShoppingCartList", response);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ShoppingCartReadDto>($"/api/Shopping/GetById/{id}");
            return View("ShoppingCartDetails", response);
        }

        [HttpGet("CheckoutCart/{cartid}")]
        public async Task<ActionResult> CheckOut(int cartid)
        {
            var response = await _httpClient.GetFromJsonAsync<ShoppingCartReadDto>($"/api/Shopping/checkOut/{cartid}");
            return View("CheckoutView", response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateShoppingCart(ShoppingAddDto shoppingAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Shopping/CreateShoppingCart", shoppingAddDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAll");
        }

        [HttpPost("AddProductToCart/{cartid}/{ProductId}")]
        public async Task<ActionResult> AddProductToCart(int cartid, int ProductId, ShoppingAddDto shoppingAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Shopping/AddProduct/{cartid}/{ProductId}", shoppingAddDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetById", new { id = cartid });
        }

        [HttpPost("UpdateCart/{id}")]
        public async Task<ActionResult> UpdateShopping(int id, ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Shopping/UpdateShopping/{id}", shoppingCartUpdateDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetById", new { id });
        }

        [HttpPost("DeleteCart/{id}")]
        public async Task<ActionResult> RemoveShoppingCart(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Shopping/RemoveShoppingCart/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAll");
        }

        [HttpPost("DeleteProduct/{cartId}/{productId}")]
        public async Task<ActionResult> RemoveProduct(int cartId, int productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Shopping/RemoveProduct/{cartId}/{productId}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetById", new { id = cartId });
        }

        [HttpPost("ApplyDiscount/{cartId}")]
        public async Task<IActionResult> ApplyDiscount(int cartId, [FromBody] string discountCode)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Shopping/{cartId}/apply-discount", discountCode);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetById", new { id = cartId });
        }
    }
}
