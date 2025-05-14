using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
//using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        public IActionResult Index()
        {
            return View();
        }

        // Get all reviews
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _httpClient.GetFromJsonAsync<List<ReviewReadDto>>("/api/Review/GetAllReviews");
            return View("ReviewList", reviews);
        }

        // Get review by ID
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _httpClient.GetFromJsonAsync<ReviewReadDto>($"/api/Review/GetById/{id}");
            return View("ReviewDetails", review);
        }

        // Get all reviews for a product
        [HttpGet]
        public async Task<IActionResult> GetReviewsByProductId(int productId)
        {
            var reviews = await _httpClient.GetFromJsonAsync<List<ReviewReadDto>>($"/api/Review/ReviewsByProduct/{productId}");
            return View("ProductReviews", reviews);
        }

        // Get number of reviews for a product
        [HttpGet]
        public async Task<IActionResult> GetReviewCount(int productId)
        {
            var count = await _httpClient.GetFromJsonAsync<int>($"/api/Review/GetReviewCount/{productId}");
            ViewBag.Count = count;
            return View("ReviewCount");
        }

        // Insert a review
        [HttpPost]
        public async Task<IActionResult> InsertReview(int productId, ReviewAddDto reviewDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Review/InsertReview/{productId}", reviewDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetReviewsByProductId", new { productId });
        }

        // Update a review
        [HttpPost]
        public async Task<IActionResult> UpdateReview(int id, ReviewUpdateDto reviewDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/Review/UpdateReview/{id}", reviewDto);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetById", new { id });
        }

        // Delete a review
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Review/DeleteReview/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction("GetAllReviews");
        }
    }
}
