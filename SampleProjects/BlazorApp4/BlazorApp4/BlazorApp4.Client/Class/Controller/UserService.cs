using Class.Controller;

namespace Class.Controller
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/User");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
