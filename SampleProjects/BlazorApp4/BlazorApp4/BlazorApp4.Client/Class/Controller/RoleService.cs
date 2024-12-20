using Class.Controller;

namespace Class.Controller
{
    public class RoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/Role");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
