using Newtonsoft.Json;
using System.Text.Json;

namespace test.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Generic method to fetch data from any API
        public async Task<T> GetDataAsync<T>(string endpoint)
        {
            //var requestBody = new
            //{
            //    query = query
            //};

            //var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}
