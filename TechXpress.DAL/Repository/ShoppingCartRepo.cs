using Microsoft.EntityFrameworkCore;
using TechXpress.DAL.Data;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly TechXpressDBContext context;

        public ShoppingCartRepo(TechXpressDBContext _context)
        {
            context = _context;
        }

        public void CreateShoppingCart(ShoppingCart shoppingCart)
        { 

                context.ShoppingCarts.Add(shoppingCart);
            SaveChanges();
               
        }

        public IQueryable<ShoppingCart> GetAllShoppingCarts()
        {
            return context.ShoppingCarts.AsNoTracking();
        }

        public ShoppingCart GetById(int cartId)
        {
            return context.ShoppingCarts
                .Include(c => c.Products)
                .FirstOrDefault(sc => sc.ShoppingCart_ID == cartId);
        }

        public void RemoveShoppingCart(ShoppingCart shoppingCart)
        {

            context.ShoppingCarts.Remove(shoppingCart);
            SaveChanges();
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            context.ShoppingCarts.Update(shoppingCart);
            SaveChanges();
        }
    }
}
