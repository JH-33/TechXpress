using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewManger reviewManger;

        public ReviewController(IReviewManger _reviewManger)
        {
            reviewManger = _reviewManger;
        }
        [HttpGet("GetAllReviews")]
        public ActionResult GetAllReviews()
        {
            return Ok(reviewManger.GetAllReviews());
        }
        [HttpGet("GetById/{Id}")]
        public ActionResult GetById(int Id)
        {

            var review = reviewManger.GetById(Id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }
        [HttpGet("ReviewsByProduct/{productid}")]
        public ActionResult GetReviewsByProductIdAsync(int productid)
        {

            var review = reviewManger.GetReviewsByProductIdAsync(productid);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [HttpGet("GetReviewCount/{productId}")]
        public ActionResult GetReviewCount(int productid)
        {

            var count = reviewManger.GetReviewCount(productid);
            if (count == 0)
                return NotFound();
            return Ok(count);
        }

        [HttpPost("InsertReview/{productId}")]
        public ActionResult InsertReview(int productId,ReviewAddDto reviewAddDto)
        {
            reviewManger.AddReviewToProduct(productId,reviewAddDto);
            return NoContent();
        }
        [HttpPut("UpdateReview/{Id}")]
        public ActionResult UpdateReview(int Id, ReviewUpdateDto reviewUpdateDto)
        {
            if (Id != reviewUpdateDto.ReviewId)
                return BadRequest();

            reviewManger.UpdateReview(Id,reviewUpdateDto);
            return NoContent();
        }
        [HttpDelete("DeleteReview/{Id}")]
        public ActionResult DeleteReview(int Id)
        {
            reviewManger?.DeleteReview(Id);
            return NoContent();
        }

    }
}
