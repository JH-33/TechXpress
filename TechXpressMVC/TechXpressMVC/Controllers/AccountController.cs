using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> GetProfilebyid(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<Profiledto>($"/api/Account/Getprofilebyid/{Id}");
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginDto logDto)
        {
            var response = await _httpClient.PostAsJsonAsync<LoginDto>($"/api/Account/Login", logDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterDto regDto)
        {
            var response = await _httpClient.PostAsJsonAsync<RegisterDto>($"/api/Account/Register", regDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpPut("{UserId}")]
        public async Task<ActionResult> UpdateProfile(string UserId, Profiledto profDto)
        {
            var response = await _httpClient.PutAsJsonAsync<Profiledto>($"/api/Account/UpdateProfile/{UserId}", profDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{UserId}")]
        public async Task<ActionResult> DeleteProfile(string UserId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Account/DeleteProfile/{UserId}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
