using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;
//using TechXpress.DAL.Migrations;

namespace TechXpress.DAL.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly TechXpressDBContext context;

        public PaymentRepo(TechXpressDBContext _context)
        {
            context = _context;
        }
        public void Delete(Payment payment)
        {
            context.Remove(payment);
        }

        public Payment GetById(int id)
        {
            return context.Payment.Find(id);
        }

        public void Insert(Payment payment)
        {
            context.Add(payment);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Payment payment)
        {
           
        }
    }
}
