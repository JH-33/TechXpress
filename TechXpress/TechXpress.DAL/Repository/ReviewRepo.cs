using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class ReviewRepo:IReviewRepo
    {
        private readonly TechXpressDBContext context;

        public ReviewRepo(TechXpressDBContext _context)
        {
            context = _context;
        }

        public void Delete(Review review)
        {
            context.Remove(review);
            context.SaveChanges();
        }
        public IQueryable<Review> GetAllReviews()
        {
            return context.Reviews;
        }

        public Review GetById(int id)
        {
            return context.Reviews.Find(id);
        }

        public IQueryable<Review> GetReviewsByProductIdAsync(int productId)
        {
            return context.Reviews.Where(r=>r.ProductID==productId);
        }

        public void Insert(Review review)
        {
            context.Add(review);
            context.SaveChanges();
        }

        public void Update(Review review)
        {
          
            context.SaveChanges();
        }
    }
}
