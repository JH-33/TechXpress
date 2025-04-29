using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class ShoppingManger:IShoppingManager
    {
        private readonly IShoppingCartRepo shoppingCartRepo;
        private readonly IProductRepo productRepo;
        

        public ShoppingManger(IShoppingCartRepo _shoppingCartRepo,IProductRepo _productRepo)
        {
            shoppingCartRepo = _shoppingCartRepo;
            productRepo = _productRepo;
           
        }

        public void AddProductToCart(int cartId, int productId,ShoppingAddDto shoppingAddDto)
        {
            var cart = shoppingCartRepo.GetById(cartId);
            if (cart==null)
            {
                CreateShoppingCart(shoppingAddDto);
                cart = shoppingCartRepo.GetAllShoppingCarts()
                    .FirstOrDefault(a => a.UserID == shoppingAddDto.UserID);
            }
            var product = productRepo.GetById(productId);
            if (product == null)
            {
                throw new Exception("product is not find");
            }
            else
            {
                cart.Products.Add(product);
                cart.NumberofItems = cart.Products.Count;
                shoppingCartRepo.Update(cart);
            }

            }

        public OrderReadDto Checkout(int cartId)
        {
            var cart = shoppingCartRepo.GetById(cartId);
            if(cart==null||cart.NumberofItems==0)
            {
                throw new Exception("cart is empty");
            }
            var order = new OrderReadDto
            {
         OrderDate =cart.CreatedDate,
        TotalAmountToPay=cart.Products.Sum(a=>a.Price),
        Order_Status= "Pending",
        Shipping_Address=cart.User?.Address,
        UserID=cart.UserID,
        ShoppingCart_ID=cart.ShoppingCart_ID
            };
            return order;

            
        }

        

        public void CreateShoppingCart(ShoppingAddDto shoppingAddDto)
        {
            var initialcart = shoppingCartRepo.GetAllShoppingCarts().FirstOrDefault(a=>a.UserID==shoppingAddDto.UserID);
            if(initialcart==null)
            {
                var cart = new ShoppingCart
                {
                    CreatedDate = DateTime.UtcNow,
                    UserID = shoppingAddDto.UserID,
                    NumberofItems = 0,
                    Products = new List<Product>()
                };
                shoppingCartRepo.CreateShoppingCart(cart);
            }
        }

        public IEnumerable<ShoppingCartReadDto> GetAllShoppingCarts()
        {
           
            var cart = shoppingCartRepo.GetAllShoppingCarts();
            var cartread = cart.Select(cart => new ShoppingCartReadDto
            {
                ShoppingCart_ID = cart.ShoppingCart_ID,
                NumberofItems = cart.NumberofItems,
                CreatedDate = cart.CreatedDate,
                UserID = cart.UserID,
                Products = cart.Products.Select(p => new ProductReadDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price
                }).ToList()
            });
            return cartread;
        }

        public ShoppingCartReadDto GetById(int cartId)
        {
            var cart = shoppingCartRepo.GetById(cartId);
            if (cart == null) return (null);
            var cartreadid = new ShoppingCartReadDto
            {
                ShoppingCart_ID = cart.ShoppingCart_ID,
                NumberofItems = cart.NumberofItems,
                CreatedDate = cart.CreatedDate,
                UserID = cart.UserID,
                Products = cart.Products.Select(p => new ProductReadDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price
                }).ToList()
            };
            return cartreadid;
        }

        public void RemoveProductFromCart(int cartId, int productId)
        {
            var cart = shoppingCartRepo.GetById(cartId);
            if (cart == null) { throw new Exception("not find cart"); }
            var product = cart.Products.FirstOrDefault(a => a.ProductId == productId);
            if (product == null) { throw new Exception("not find product"); }
            cart.Products.Remove(product);
            cart.NumberofItems = cart.Products.Count;
            shoppingCartRepo.Update(cart);

        }

        public void RemoveShoppingCart(int cartId)
        {
            var cart = shoppingCartRepo.GetById(cartId);
            if (cart == null) throw new Exception();

            shoppingCartRepo.RemoveShoppingCart(cart);
            
        }

        public void Update(ShoppingCartUpdateDto shoppingCartUpdateDto)
        {
            var cart = shoppingCartRepo.GetById(shoppingCartUpdateDto.ShoppingCart_ID);
            if (cart == null) throw new Exception();

            if (shoppingCartUpdateDto.NumberofItems.HasValue)
                cart.NumberofItems = shoppingCartUpdateDto.NumberofItems.Value;

            shoppingCartRepo.Update(cart);
            
        }

    }
}
