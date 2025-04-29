using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface IReviewRepo
    {
        IQueryable<Review> GetAllReviews();
        Review GetById(int id);
        void Insert(Review review);
        void Update(Review review);
        void Delete(Review review);
        IQueryable<Review>GetReviewsByProductIdAsync(int productId);
    }
}
