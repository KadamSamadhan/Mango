namespace Mango.Services.ShoppingCartAPI.Models.Dto
{
    public class CouponDto
    {
        private int couponID;
        private string couponCode;
        private double discountAmount;
        private int minAmount;
        public int CouponID { get => couponID; set => couponID = value; }
        public string CouponCode { get => couponCode; set => couponCode = value; }
        public double DiscountAmount { get => discountAmount; set => discountAmount = value; }
        public int MinAmount { get => minAmount; set => minAmount = value; }
    }
}
