using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
    public class Coupon
    {
        private int couponID;
        private string couponCode;
        private double discountAmount;
        private int minAmount;
        [Key]
        public int CouponID { get => couponID; set => couponID = value; }
        [Required]
        public string CouponCode { get => couponCode; set => couponCode = value; }
        [Required]
        public double DiscountAmount { get => discountAmount; set => discountAmount = value; }
       
        public int MinAmount { get => minAmount; set => minAmount = value; }
    }
}
