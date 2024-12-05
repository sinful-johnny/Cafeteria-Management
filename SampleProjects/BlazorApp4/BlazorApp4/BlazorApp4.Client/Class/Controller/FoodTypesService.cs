namespace Class.Controller
{
    public class FoodTypesService
    {
        private readonly HttpClient _httpClient;

        public FoodTypesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetFoodsAsync()
        {
            var response = await _httpClient.GetAsync($"{HttpInfo.Domain}/api/FoodType");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
