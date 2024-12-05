using System.Net.Http;
using System.Threading.Tasks;

namespace Class.Controller
{
    public class TableTypesService
    {
        private readonly HttpClient _httpClient;

        public TableTypesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetTablesAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/ShapeType");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}

