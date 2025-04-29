using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public interface IReviewManger
    {
        IEnumerable<ReviewReadDto> GetAllReviews();
        ReviewReadDto GetById(int id);
        IEnumerable<ReviewReadDto> GetReviewsByProductIdAsync(int productId);
        void AddReviewToProduct(int productId, ReviewAddDto reviewAddDto);
        void UpdateReview(int productId, ReviewUpdateDto reviewUpdateDto);
        void DeleteReview(int reviewId);
    }
}
