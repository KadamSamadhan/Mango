using Microsoft.AspNetCore.Authentication;

namespace Mango.Services.ShoppingCartAPI.Utility
{

    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _contextAccessor = accessor;

        }
       protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var _token = await _contextAccessor.HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            return await base.SendAsync(request,cancellationToken);
        }
    }
}
