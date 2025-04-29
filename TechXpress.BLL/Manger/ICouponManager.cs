using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.API.Controllers
{
    public interface ICouponManager
    {
        IEnumerable<CouponReadDto> GetAllActive();
        CouponReadDto GetByCode(string name);
        CouponReadDto GetByID(int ID);
        void UpdateCoupon(CouponUpdateDto couponupdatedto);

        void AddCoupon(CouponAddDto CouponAddDto);

        //void ApplyCoupontoCart( );
        void DeleteCoupon(int code);
       

    }
}
