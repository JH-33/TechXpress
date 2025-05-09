using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public ShoppingController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ShoppingCartReadDto>>("/api/Shopping/GetAllshoppingcart");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<ShoppingCartReadDto>($"/api/Shopping/GetById/{Id}");
            return Ok(response);
        }

        [HttpGet("{cartid}")]
        public async Task<ActionResult> CheckOut(int cartid)
        {
            var response = await _httpClient.GetFromJsonAsync<ShoppingCartReadDto>($"/api/Shopping/checkOut/{cartid}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateShoppingCart(ShoppingAddDto shoppingAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync<ShoppingAddDto>($"/api/Shopping/CreateShoppingCart", shoppingAddDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpPost("{cartid}/{ProductId}")]
        public async Task<ActionResult> AddProductToCart(int cartid, int ProductId, ShoppingAddDto shoppingAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync<ShoppingAddDto>($"/api/Shopping/AddProduct/{cartid}/{ProductId}", shoppingAddDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateShopping(int Id, ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync<ShoppingCartUpdateDto>($"/api/Shopping/UpdateShopping/{Id}", shoppingCartUpdateDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveShoppingCart(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Shopping/RemoveShoppingCart/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{CartId}/{ProductId}")]
        public async Task<ActionResult> RemoveProduct(int CartId, int ProductId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Shopping/RemoveProduct/{CartId}/{ProductId}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
