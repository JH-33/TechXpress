using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
//using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        // Main view
        public IActionResult Index()
        {
            return View();
        }

        // Get all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductReadDto>>("/api/Product");
            return View("ProductList", response); // You can change to "return Ok(response);" if not using Views
        }

        // Get product by ID
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductReadDto>($"/api/Product/{id}");
            return View("Details", product);
        }

        // Get product by Name
        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _httpClient.GetFromJsonAsync<ProductReadDto>($"/api/Product/ByName/{name}");
            return View("Details", product);
        }

        // Get best-selling product (based on least stock)
        [HttpPost]
        public async Task<IActionResult> GetBestProduct(List<ProductAddDto> products)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Product/GetBestProduct", products);
            response.EnsureSuccessStatusCode();

            var best = await response.Content.ReadFromJsonAsync<ProductAddDto>();
            return View("BestProduct", best);
        }

        // Add new product
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Product", productDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        // Update product
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Product/{id}", productDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        // Delete product
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Product/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        // Update product stock quantity
        [HttpPost]
        public async Task<IActionResult> UpdateStock(int productId, int quantity, bool isIncrease)
        {
            var requestUri = $"/api/Product/UpdateStock?productId={productId}&quantity={quantity}&isIncrease={isIncrease}";
            var response = await _httpClient.PostAsync(requestUri, null); // no body
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            ViewBag.Message = result;
            return View("StockUpdated");
        }
    }
}
