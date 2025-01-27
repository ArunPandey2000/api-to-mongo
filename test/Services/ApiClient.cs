using Newtonsoft.Json;
using System.Text;
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
        public async Task<T> GetDataAsync<T>(string endpoint, string query)
        {
            var requestBody = new
            {
                query
            };

            // Serialize the GraphQL query into JSON
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            // Send a POST request with the query
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json)!;
        }
    }
}
