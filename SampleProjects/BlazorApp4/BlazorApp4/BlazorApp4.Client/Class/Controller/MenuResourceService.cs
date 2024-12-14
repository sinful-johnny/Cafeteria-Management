namespace Class.Controller
{
    public class MenuResourceService
    {
        private readonly HttpClient _httpClient;

        public MenuResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResourceAsync(string MenuName)
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/MenuResources/{MenuName}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        //public async Task<string> PostTableAsync(string TableListJson)
        //{
        //    var content = new StringContent(TableListJson, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"{HttpInfo.Domain}/api/TableFoods", content);
        //    response.EnsureSuccessStatusCode();

        //    return await response.Content.ReadAsStringAsync();
        //}
    }
}
