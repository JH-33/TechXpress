using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TechXpress.BLL.DTO;
using TechXpress.DAL.Data.Models;
using TechXpress.DAL.Repository;

namespace TechXpress.API.Controllers
{
    public class CouponManager : ICouponManager
    {
        private readonly ICouponRepo couponRepo;
        public CouponManager(ICouponRepo _couponrepo)
        {
            couponRepo = _couponrepo;
        }
        public void AddCoupon(CouponAddDto CouponAddDto)
        {

            if (CouponAddDto.ExpirationDate.Date < DateTime.Now.Date)
            {
                return;
            }
            var coupon = new Coupon
            {
                Code = CouponAddDto.Code,
                ExpirationDate = CouponAddDto.ExpirationDate,
                UsageLimit = CouponAddDto.UsageLimit,
                couponDiscountinPercentage = CouponAddDto.couponDiscountinPercentage


            };
            couponRepo.Insert(coupon);
            couponRepo.SaveChanges();
        }

        public void DeleteCoupon(int code)
        {
            var deletedcoupon = couponRepo.GetById(code);
            if (deletedcoupon == null)
            {
                return;
            }
            couponRepo.Delete(deletedcoupon);

        }

        public IEnumerable<CouponReadDto> GetAllActive()
        {
            var res = couponRepo.GetAllActive();
            var coupons = res.Select(a => new CouponReadDto
            {
                Code = a.Code,
                ExpirationDate = a.ExpirationDate,
                UsageLimit = a.UsageLimit,
                couponDiscountinPercentage = a.couponDiscountinPercentage,
                UsageCount = a.UsageCount,

            }
            ).ToList();
            return coupons;
        }

        public CouponReadDto GetByID(int name)
        {
            var couponexisting = couponRepo.GetById(name);
            if (couponexisting == null)
            {
                return null;
            }
            var coupon = new CouponReadDto
            {
                //CouponID = couponexisting.CouponID,
                Code = couponexisting.Code,
                ExpirationDate = couponexisting.ExpirationDate,
                UsageLimit = couponexisting.UsageLimit,
                UsageCount = couponexisting.UsageCount,
                couponDiscountinPercentage = couponexisting.couponDiscountinPercentage
            };
            return coupon;
        }


        public CouponReadDto GetByCode(string name)
        {
            var couponexisting = couponRepo.GetByCode(name);
            if (couponexisting == null)
            {
                return null;
            }
            var coupon = new CouponReadDto
            {

                Code = couponexisting.Code,
                ExpirationDate = couponexisting.ExpirationDate,
                UsageLimit = couponexisting.UsageLimit,
                UsageCount = couponexisting.UsageCount,
                couponDiscountinPercentage = couponexisting.couponDiscountinPercentage
            };
            return coupon;
        }

        public void UpdateCoupon(CouponUpdateDto couponupdatedto)
        {
            var oldcoupon = couponRepo.GetByCode(couponupdatedto.Code);
            if (oldcoupon == null)
            {
                return;
            }
            //oldcoupon.CouponID = couponupdatedto.CouponID;
            oldcoupon.Code = couponupdatedto.Code;
            oldcoupon.ExpirationDate = couponupdatedto.ExpirationDate;
            oldcoupon.UsageLimit = oldcoupon.UsageLimit;
            oldcoupon.couponDiscountinPercentage = oldcoupon.couponDiscountinPercentage;


            couponRepo.Update(oldcoupon);
            couponRepo.SaveChanges();
        }

    }
}
