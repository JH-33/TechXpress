using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.BLL.Manger
{
    public class PaymentManger:IpaymentManger
    {
        private readonly IPaymentRepo paymentRepo;

        public PaymentManger(IPaymentRepo _paymentRepo)
        {
            paymentRepo = _paymentRepo;
        }

        public void Delete(int id)
        {
            var modeldelete = paymentRepo.GetById(id);
            if (modeldelete == null) throw new Exception("");
            paymentRepo.Delete(modeldelete);
            
        }

        public PaymentReadDto GetById(int id)
        {
            var modelread = paymentRepo.GetById(id);
            if (modelread == null) return null;
            var modelreaddro = new PaymentReadDto()
            {
                PaymentID = modelread.PaymentID,
                PaymentType=modelread.PaymentType,
                PaymentAmount=modelread.PaymentAmount,
                PaymentDate=modelread.PaymentDate,
                OrderID=modelread.OrderID
            };
            return modelreaddro;
        }

        public void Insert(PaymentAddDto paymentAdd)
        {
            var modeladd = new Payment()
            {
                PaymentType = paymentAdd.PaymentType,
                PaymentAmount = paymentAdd.PaymentAmount,
                PaymentDate = paymentAdd.PaymentDate,
                OrderID = paymentAdd.OrderID
            };
            paymentRepo.Insert(modeladd);
        }

        

        public void Update(PaymentUpdateDto paymentUpdate)
        {
            var modelupdate = paymentRepo.GetById(paymentUpdate.PaymentID);
            if (modelupdate == null) throw new Exception("");
            modelupdate.PaymentType = paymentUpdate.PaymentType;
            modelupdate.PaymentAmount = paymentUpdate.PaymentAmount;
            modelupdate.PaymentDate = paymentUpdate.PaymentDate;
            modelupdate.OrderID = paymentUpdate.OrderID;
            paymentRepo.Update(modelupdate);
            
    }
    }
}
