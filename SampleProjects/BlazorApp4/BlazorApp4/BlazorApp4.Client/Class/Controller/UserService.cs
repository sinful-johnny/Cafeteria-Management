using Class.Controller;
using System.Net.Http.Json;

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

        public async Task<HttpResponseMessage> AddRoleAsync(string userId, string roleId)
        {
            var response = await _httpClient.PostAsJsonAsync($"{HttpInfo.Domain}/api/User/add-role/{userId}", new UserRole { roleId = roleId});
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> RemoveRoleAsync(string userId, string roleId)
        {
            var response = await _httpClient.DeleteAsync($"{HttpInfo.Domain}/api/User/remove-role/{userId}/{roleId}");
            response.EnsureSuccessStatusCode();

            return response;
        }


        private class UserRole
        {
            public string roleId { get; set; }
        }
    }
}
