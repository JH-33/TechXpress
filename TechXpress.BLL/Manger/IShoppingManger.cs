using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{

        public interface IShoppingManager
        {
            IEnumerable<ShoppingCartReadDto> GetAllShoppingCarts();
            ShoppingCartReadDto GetById(int cartId);
            void CreateShoppingCart(ShoppingAddDto shoppingAddDto);
            void Update(ShoppingCartUpdateDto shoppingCartUpdateDto);
            void RemoveShoppingCart(int cartId);
            void AddProductToCart(int cartId, int productId,ShoppingAddDto shoppingAddDto );
            void RemoveProductFromCart(int cartId, int productId);
           OrderReadDto Checkout(int cartId);
        }

    
}
