using Microsoft.AspNetCore.Mvc;
using TechXpressMVC.DTOs;

namespace TechXpressMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HttpClient _httpClient;
        public IActionResult Index()
        {
            return View();
        }

        public ReviewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7011");
        }

        [HttpGet]

        public async Task<ActionResult> GetAllReviews()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ReviewReadDto>>("/api/Review/GetAllReviews");
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id)
        {
            var response = await _httpClient.GetFromJsonAsync<ReviewReadDto>($"/api/Review/GetById/{Id}");
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetReviewsByProductIdAsyn(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ReviewReadDto>($"/api/Review/ReviewsByProduct/{productId}");
            return Ok(response);
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult> GetReviewCount(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<ReviewReadDto>($"/api/Review/GetReviewCount/{productId}");
            return Ok(response);
        }

        [HttpPost("{productId}")]
        public async Task<ActionResult> InsertReview(int productId, ReviewAddDto reviewDto)
        {
            var response = await _httpClient.PostAsJsonAsync<ReviewAddDto>($"/api/Review/InsertReview/{productId}", reviewDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }


        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateReview(int Id, ReviewUpdateDto reviewDto)
        {
            var response = await _httpClient.PutAsJsonAsync<ReviewUpdateDto>($"/api/Review/UpdateReview/{Id}", reviewDto);
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Review/DeleteReview/{Id}");
            response.EnsureSuccessStatusCode();

            return Ok(response.Content.ReadAsStringAsync().IsCompletedSuccessfully);
        }
    }
}
