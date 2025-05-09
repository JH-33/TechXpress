using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<OrderReadDto>>("/api/Order");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<OrderReadDto>($"/api/Order/{Id}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(OrderAddDto orderDto)
        {
            var response = await _httpClient.PostAsJsonAsync<OrderAddDto>($"/api/Order", orderDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, OrderUpdateDto orderDto)
        {
            var response = await _httpClient.PutAsJsonAsync<OrderUpdateDto>($"/api/Order/{Id}", orderDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Order/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
