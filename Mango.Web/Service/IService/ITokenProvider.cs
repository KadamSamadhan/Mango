namespace Mango.Web.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(String token);
        string? GetToken();
        void ClearToken();

    }
}
