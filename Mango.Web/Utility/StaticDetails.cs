namespace Mango.Web.Utility
{
    public class StaticDetails
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string ShopingCartAPIBase { get; set; }
        public static string RoleAdmin { get; set; } = "ADMIN";
        public static string RoleCustomer { get; set; } = "CUSTOMER";
        public static string TokenCookie { get; set; } = "JwtToken";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,

        }
    }
}
