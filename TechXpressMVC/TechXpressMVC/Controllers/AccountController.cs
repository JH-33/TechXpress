using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TechXpress.BLL.DTO.AccountDto;
using System.Text.Json;

namespace TechXpressMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        public IActionResult Index() => View();
        public IActionResult Login() => View();
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto logDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Account/Login", logDto);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Login failed");
                return View();
            }

            var token = await response.Content.ReadAsStringAsync();
            HttpContext.Session.SetString("JWToken", token);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto regDto)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Account/Register", regDto);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Registration failed");
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/api/Account/GetProfile");
            if (!response.IsSuccessStatusCode) return View("Error");

            var profile = await response.Content.ReadFromJsonAsync<Profiledto>();
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Profiledto profDto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync("/api/Account/UpdateProfile", profDto);
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("GetProfile");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProfile()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync("/api/Account/DeleteProfile");
            if (!response.IsSuccessStatusCode) return View("Error");

            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleDto dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync("/api/Account/AssignRoleToUser", dto);
            if (!response.IsSuccessStatusCode) return View("Error");

            return RedirectToAction("GetProfile");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login");
        }
    }
}
