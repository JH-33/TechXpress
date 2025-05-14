using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
//using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011"); // Set the base address of the API
        }

        // Optional: If you are using JWT authentication
        private void AddJwtHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Show main page (index)
        public IActionResult Index()
        {
            return View();
        }

        // Get payment by ID
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            AddJwtHeader();
            var payment = await _httpClient.GetFromJsonAsync<PaymentReadDto>($"/api/Payment/{id}");

            if (payment == null)
                return View("NotFound");

            return View("Details", payment); // View: Views/Payment/Details.cshtml
        }

        // Show form for creating payment
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // View: Views/Payment/Create.cshtml
        }

        // Submit a new payment
        [HttpPost]
        public async Task<IActionResult> Create(PaymentAddDto payDto)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("/api/Payment", payDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create payment.");
                return View(payDto);
            }

            return RedirectToAction("Index");
        }

        // Show form for updating a payment
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddJwtHeader();
            var payment = await _httpClient.GetFromJsonAsync<PaymentReadDto>($"/api/Payment/{id}");

            if (payment == null)
                return View("NotFound");

            // Map to PaymentUpdateDto if needed
            return View("Edit", payment); // View: Views/Payment/Edit.cshtml
        }

        // Submit payment update
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PaymentUpdateDto payDto)
        {
            AddJwtHeader();
            if (id != payDto.PaymentID)
            {
                ModelState.AddModelError("", "ID mismatch.");
                return View(payDto);
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/Payment/{id}", payDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update payment.");
                return View(payDto);
            }

            return RedirectToAction("Index");
        }

        // Delete payment
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"/api/Payment/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to delete payment.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Payment deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
