using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class ReviewManger:IReviewManger
    {
        private readonly IReviewRepo reviewRepo;

        public ReviewManger(IReviewRepo reviewRepo)
        {
            this.reviewRepo = reviewRepo;
        }

        public void AddReviewToProduct(int productId, ReviewAddDto reviewAddDto)
        {
            var model = new Review
            {
                ProductID = productId, 
                ReviewDescription = reviewAddDto.ReviewDescription,
                Rating = reviewAddDto.Rating,
                UserID = reviewAddDto.UserID,
            };

            reviewRepo.Insert(model);
        }

        public int GetReviewCount(int productId)
        {
            return GetReviewsByProductIdAsync(productId).Count();
        }

        public void DeleteReview(int id)
        {
            var reviewdelete = reviewRepo.GetById(id);
            if (reviewdelete == null) throw new Exception("not found review");
            reviewRepo.Delete(reviewdelete);
        }

        public IEnumerable<ReviewReadDto> GetAllReviews()
        {
            var data = reviewRepo.GetAllReviews().ToList();
            var ReviewdDto = data.Select(a => new ReviewReadDto
            {
                ReviewDescription = a.ReviewDescription,
                ReviewId = a.Id,
                Rating = a.Rating,
                UserID = a.UserID,
                ProductID = a.ProductID

            }).ToList();
            return ReviewdDto;
        }

        public ReviewReadDto GetById(int id)
        {
            var data = reviewRepo.GetById(id);
            if (data == null) return null;
            var reviewreaddto = new ReviewReadDto
                {
                ReviewDescription = data.ReviewDescription,
                ReviewId = data.Id,
                Rating = data.Rating,
                UserID = data.UserID,
                ProductID = data.ProductID

            };
            return reviewreaddto;
        }

        public IEnumerable<ReviewReadDto> GetReviewsByProductIdAsync(int productId)
        {
            var data = reviewRepo.GetReviewsByProductIdAsync(productId).OrderByDescending(ra => ra.Rating);
            
            
            var reviewreaddto = data .Select(a=>new ReviewReadDto
            {
                ReviewDescription = a.ReviewDescription,
                ReviewId = a.Id,
                Rating = a.Rating,
                UserID = a.UserID,
                ProductID = a.ProductID

            });
            return reviewreaddto;

        }

        public void UpdateReview(int productId, ReviewUpdateDto reviewUpdateDto)
        {
            var reviewupdate = reviewRepo.GetById(reviewUpdateDto.ReviewId);
            if (reviewupdate == null) throw new Exception("review not Found");

            reviewupdate.ReviewDescription = reviewUpdateDto.ReviewDescription;
            reviewupdate.Rating = reviewUpdateDto.Rating;

            reviewRepo.Update(reviewupdate);
        }
    }
}
