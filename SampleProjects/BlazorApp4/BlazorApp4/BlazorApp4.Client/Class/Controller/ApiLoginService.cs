using System.Net.Http.Json;

namespace Class.Controller
{
    public class ApiLoginService
    {
        private readonly HttpClient _httpClient;

        public ApiLoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(string username, string password)
        {
            var creds = new Credentials { userName = username, password = password };
            var response = await _httpClient.PostAsJsonAsync($"{HttpInfo.Domain}/api/Account/login", creds);
            response.EnsureSuccessStatusCode();

            return response;
        }

        private class Credentials
        {
            public required string userName { get; set; }
            public required string password { get; set; }
        }
    }
}
