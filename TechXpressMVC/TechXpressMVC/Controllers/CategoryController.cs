using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("/api/Category");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetBy(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/Category/GetBy{Id}");
            return Ok(response);
        }

        [HttpGet("{Name}")]
        public async Task<ActionResult> GetByName(string Name)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/Category/GetName{Name}");
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> Insert(CategoryDto categoryDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CategoryDto>($"/api/Category", categoryDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, CategoryDto categoryDto)
        {
            var response = await _httpClient.PutAsJsonAsync<CategoryDto>($"/api/Category/{Id}", categoryDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Category/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

    }
}
