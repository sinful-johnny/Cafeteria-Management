using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace Class.Auth
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<AuthorizationMessageHandler> _logger;
        private readonly string _cookieName = "ApiAuth";
        private readonly string _identityCookieName = ".AspNetCore.Identity.Application";

        public AuthorizationMessageHandler(IHttpContextAccessor httpContextAccessor, IJSRuntime jsRuntime, ILogger<AuthorizationMessageHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Sending HTTS request!");
            //await _jsRuntime.InvokeVoidAsync("setCookie", _cookieName, "iodjsaoivnoujewioqunsajbncu219083u21ihdisua12", 3);

            try
            {
                string token = string.Empty;
                try
                {
                    token = await _jsRuntime.InvokeAsync<string>("getCookie", _cookieName);
                    _logger.LogInformation($"Retrieving token: {token}");
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"Exception: {e.ToString()}");
                }

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    try
                    {
                        _httpContextAccessor.HttpContext.Response.Cookies.Delete(_cookieName);
                    }catch (Exception e)
                    {
                        _logger.LogInformation($"Exception: {e.ToString()}");
                    }

                    //await _jsRuntime.InvokeVoidAsync("location.replace", "/Account/Login");
                }

                _logger.LogInformation($"HTTPS response: {response.Content}");

                return response;
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Exception: {e.ToString()}");
            }
            return null;
        }
    }
}
