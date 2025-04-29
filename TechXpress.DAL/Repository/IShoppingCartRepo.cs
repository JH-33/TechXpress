using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.DAL.Data.Models;

namespace TechXpress.DAL.Repository
{
    public interface IShoppingCartRepo
    {
        IQueryable<ShoppingCart> GetAllShoppingCarts();
        ShoppingCart GetById(int cartId);
        void CreateShoppingCart(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        void RemoveShoppingCart(ShoppingCart shoppingCart);
        void SaveChanges();

    }
}
