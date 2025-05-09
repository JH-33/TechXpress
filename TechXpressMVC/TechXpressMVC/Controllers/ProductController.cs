using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public ProductController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ProductReadDto>>("/api/Product/Product");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductReadDto>($"/api/Product/Product/{Id}");
            return Ok(response);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult> GetByName(string Name)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductReadDto>($"/api/Product/GetProduct/{Name}");
            return Ok(response);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult> GetFastSellingProduct(string Name)
        {
            var response = await _httpClient.GetFromJsonAsync<ProductReadDto>($"/api/Product/GetBestProduct/{Name}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductAddDto productAddDto)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductAddDto>($"/api/Product/AddProduct", productAddDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateProduct(int Id, ProductUpdateDto doctorUpdateDto)
        {
            var response = await _httpClient.PutAsJsonAsync<ProductUpdateDto>($"/api/Product/UpdateProduct/{Id}", doctorUpdateDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Product/DeleteProduct/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

    }
}
