using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TechXpressMVC.DTO;

namespace TechXpressMVC.Controllers
{
    public class DiscountController : Controller
    {
        private readonly HttpClient _httpClient;

        public DiscountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011"); // Base URL of the Web API
        }

        // Add JWT token to request headers if available (for authorized actions)
        private void AddJwtHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Main index page (optional)
        public IActionResult Index()
        {
            return View();
        }

        // Get all discounts and display them in a view
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _httpClient.GetFromJsonAsync<List<DiscountDto>>("/api/Discount");
            return View("DiscountList", discounts); // View should display list of discounts
        }

        // Return the form to create a new discount
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle POST request to create a discount
        [HttpPost]
        public async Task<IActionResult> Create(DiscountDto discount)
        {
            AddJwtHeader(); // Add token to the request for authorization

            var response = await _httpClient.PostAsJsonAsync("/api/Discount", discount);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to add discount");
                return View(discount);
            }

            TempData["Success"] = "Discount code added successfully";
            return RedirectToAction("GetAll");
        }

        // Validate a discount code by calling the API and returning a result view
        [HttpGet]
        public async Task<IActionResult> CheckCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "Please enter a code");
                return View("ValidateDiscount");
            }

            var response = await _httpClient.GetAsync($"/api/Discount/{code}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Code = code;
                ViewBag.IsValid = false; // Discount code is not valid
                return View("ValidateDiscount");
            }

            var discount = await response.Content.ReadFromJsonAsync<DiscountDto>();
            ViewBag.Code = code;
            ViewBag.IsValid = true; // Discount code is valid
            ViewBag.Discount = discount;
            return View("ValidateDiscount");
        }
    }
}
