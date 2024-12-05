using Class.Converter;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Class.Controller
{
    public class TableService
    {
        private readonly HttpClient _httpClient;

        public TableService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTablesAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/TableFoods");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostTableAsync(string TableListJson)
        {
            var content = new StringContent(TableListJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{HttpInfo.Domain}/api/TableFoods", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
