using System.Net.Http.Json;

namespace Class.Controller
{
    public class RoleMenuPermController
    {
        private readonly HttpClient _httpClient;

        public RoleMenuPermController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetRoleMenuPermControllerAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/RoleMenuPerm");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> AddRoleMenuPermAsync(string RoleId, int MenuId, int PermId)
        {
            var body = new PermDto { permId = PermId };
            var response = await _httpClient.PostAsJsonAsync($"{HttpInfo.Domain}/api/RoleMenuPerm/RoleMenuPerm/Insert?roleId={RoleId}&menuId={MenuId}", body);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteRoleMenuPermAsync(string RoleId, int MenuId, int PermId)
        {
            var response = await _httpClient.DeleteAsync($"{HttpInfo.Domain}/api/RoleMenuPerm/RoleMenuPerm/Delete?roleId={RoleId}&menuId={MenuId}&permId={PermId}"); 
            return response;
        }

        private class PermDto
        {
            public required int permId { get; set; }
        }
    }
}
