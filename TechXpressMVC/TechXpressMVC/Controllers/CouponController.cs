using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class CouponController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public CouponController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAllActive()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CouponReadDto>>("/api/Coupon");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetByCode(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<CouponReadDto>($"/api/Coupon/{Id}");
            return Ok(response);
        }

     


        [HttpPost]
        public async Task<ActionResult> InsertCoupon(CouponAddDto couponDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CouponAddDto>($"/api/Coupon", couponDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{code}")]
        public async Task<ActionResult> UpdateCoupon(string code, CouponUpdateDto couponDto)
        {
            var response = await _httpClient.PutAsJsonAsync<CouponUpdateDto>($"/api/Coupon/{code}", couponDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteCoupon(string code)
        {
            var response = await _httpClient.DeleteAsync($"/api/Coupon/{code}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
