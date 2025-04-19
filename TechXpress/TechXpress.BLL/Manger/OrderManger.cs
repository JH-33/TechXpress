using TechXpress.BLL.DTO;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class OrderManger : IOrderManger
    {
        private readonly IOrderrepo orderrepo;

        public OrderManger(IOrderrepo _orderrepo)
        {
            orderrepo = _orderrepo;
        }
        public void Delete(int id)
        {
            var modeldelet = orderrepo.GetById(id);
            orderrepo.Delete(modeldelet);
            SaveChanges();
        }

        public IEnumerable<OrderReadDto> GetAll()
        {
            var modelread = orderrepo.GetAll();
            var orderread = modelread.Select(a => new OrderReadDto
            {
                OrderID = a.OrderID,
                OrderDate = a.OrderDate,
                TotalAmountToPay = a.TotalAmountToPay,
                Order_Status = a.Order_Status,
                Shipping_Address = a.Shipping_Address,
                UserID = a.UserID,
                PaymentID = a.PaymentID,
                ShoppingCart_ID = a.ShoppingCart_ID
            }).ToList();
            return (orderread);
        }

        public OrderReadDto GetById(int id)
        {
            var modelget = orderrepo.GetById(id);
            var orderreaddto = new OrderReadDto()
            {
                OrderID = modelget.OrderID,
                OrderDate = modelget.OrderDate,
                TotalAmountToPay = modelget.TotalAmountToPay,
                Order_Status = modelget.Order_Status,
                Shipping_Address = modelget.Shipping_Address,
                UserID = modelget.UserID,
                PaymentID = modelget.PaymentID,
                ShoppingCart_ID = modelget.ShoppingCart_ID
            };
            return orderreaddto;
        }

        public void Insert(OrderAddDto orderAddDto)
        {
            var model3 = new Order()
            {
                Shipping_Address = orderAddDto.Shipping_Address,
                UserID = orderAddDto.UserID,
                PaymentID = orderAddDto.PaymentID,
                ShoppingCart_ID = orderAddDto.ShoppingCart_ID

            };
            orderrepo.Insert(model3);

        }

        public void SaveChanges()
        {
            orderrepo.SaveChanges();
        }

        public void Update(OrderUpdateDto orderUpdateDto)
        {
            var model4 = orderrepo.GetById(orderUpdateDto.OrderID);
            model4.Shipping_Address = orderUpdateDto.Shipping_Address;
            model4.OrderDate = orderUpdateDto.OrderDate;
            model4.Order_Status = orderUpdateDto.Order_Status;
            model4.TotalAmountToPay = orderUpdateDto.TotalAmountToPay;
            model4.PaymentID = orderUpdateDto.PaymentID;
            model4.ShoppingCart_ID = orderUpdateDto.ShoppingCart_ID;
        
            orderrepo.Update(model4);
        }
    }
}
