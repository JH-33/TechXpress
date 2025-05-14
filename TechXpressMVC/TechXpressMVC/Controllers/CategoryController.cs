using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;

namespace TechXpressMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        private void AddJwtHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CategoryDto>>("/api/Category");
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/Category/{id}");
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/Category/ByName/{name}");
            return View("Details", response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("/api/Category", categoryDto);
            if (!response.IsSuccessStatusCode)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"/api/Category/{id}");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id) return BadRequest("ID mismatch.");

            AddJwtHeader();
            var response = await _httpClient.PutAsJsonAsync($"/api/Category/{id}", categoryDto);
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"/api/Category/{id}");
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("Index");
        }
    }

}
