using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class DiscountManager : IDiscountManager
    {
        private readonly IDiscountRepo _repo;

        public DiscountManager(IDiscountRepo repo)
        {
            _repo = repo;
        }

        public Discount GetValidDiscount(string code)
        {
            return _repo.GetByCode(code);
        }

        public void CreateDiscount(Discount discount)
        {
            _repo.Add(discount);
        }

        public IEnumerable<Discount> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
