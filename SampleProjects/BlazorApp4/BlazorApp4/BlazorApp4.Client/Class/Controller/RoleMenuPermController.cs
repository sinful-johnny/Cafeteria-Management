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

        public async Task<string> AddRoleMenuPermControllerAsync(string RoleId, string MenuId, string PermId)
        {
            var body = new PermDto { permId = PermId };
            var response = await _httpClient.PostAsJsonAsync($"{HttpInfo.Domain}/api/RoleMenuPerm/?roleId={RoleId}&menuId={MenuId}", body);
            return await response.Content.ReadAsStringAsync();
        }

        //public async Task<string> DeleteRoleMenuPermControllerAsync(string RoleId, string MenuId, string PermId)
        //{
        //    var body = new PermDto { permId = PermId };
        //    var response = await _httpClient.DeleteAsync($"{HttpInfo.Domain}/api/RoleMenuPerm/?roleId={RoleId}&menuId={MenuId}", body);
        //    return await response.Content.ReadAsStringAsync();
        //}

        private class PermDto
        {
            public required string permId { get; set; }
        }
    }
}
