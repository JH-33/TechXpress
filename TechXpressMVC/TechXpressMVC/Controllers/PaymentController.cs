using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public PaymentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<PaymentReadDto>($"/api/Payment/{Id}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(PaymentAddDto payDto)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentAddDto>($"/api/Payment", payDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, PaymentUpdateDto payDto)
        {
            var response = await _httpClient.PutAsJsonAsync<PaymentUpdateDto>($"/api/Payment/{Id}", payDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Payment/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
