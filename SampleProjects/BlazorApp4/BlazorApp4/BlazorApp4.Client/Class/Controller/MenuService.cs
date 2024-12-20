using Class.Controller;

namespace Class.Controller
{
    public class MenuService
    {
        private readonly HttpClient _httpClient;

        public MenuService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetMenusAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/Menu");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
