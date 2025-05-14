using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
//using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011"); // API base URL
        }

        // Helper method to attach JWT token from session if needed
        private void AddJwtHeader()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Main page for orders
        public IActionResult Index()
        {
            return View();
        }

        // Get all orders from API
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            AddJwtHeader();
            var orders = await _httpClient.GetFromJsonAsync<List<OrderReadDto>>("/api/Order");
            return View("OrderList", orders); // You should have a view named OrderList.cshtml
        }

        // Get specific order by ID
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            AddJwtHeader();
            var order = await _httpClient.GetFromJsonAsync<OrderReadDto>($"/api/Order/{id}");
            if (order == null)
                return View("NotFound"); // Or any custom error view

            return View("OrderDetails", order); // You should have OrderDetails.cshtml
        }

        // Show form to create a new order
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // View should contain a form for OrderAddDto
        }

        // Submit new order
        [HttpPost]
        public async Task<IActionResult> Create(OrderAddDto orderDto)
        {
            AddJwtHeader();
            var response = await _httpClient.PostAsJsonAsync("/api/Order", orderDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create order.");
                return View(orderDto);
            }

            return RedirectToAction("GetAll");
        }

        // Show form to edit an existing order
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AddJwtHeader();
            var order = await _httpClient.GetFromJsonAsync<OrderReadDto>($"/api/Order/{id}");
            if (order == null)
                return View("NotFound");

            // Map to OrderUpdateDto if needed before sending to view
            return View(order); // You should have Edit.cshtml view
        }

        // Submit order update
        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderUpdateDto orderDto)
        {
            AddJwtHeader();
            if (id != orderDto.OrderID)
            {
                ModelState.AddModelError("", "ID mismatch.");
                return View(orderDto);
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/Order/{id}", orderDto);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update order.");
                return View(orderDto);
            }

            return RedirectToAction("GetAll");
        }

        // Delete an order
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            AddJwtHeader();
            var response = await _httpClient.DeleteAsync($"/api/Order/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Failed to delete order.";
                return RedirectToAction("GetAll");
            }

            TempData["Success"] = "Order deleted successfully.";
            return RedirectToAction("GetAll");
        }
    }
}
