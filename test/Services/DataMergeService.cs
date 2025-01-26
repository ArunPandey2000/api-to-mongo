using test.Models;

namespace test.Services
{
    public class DataMergeService
    {
        private readonly ApiClient _apiClient;
        private readonly MongoService _mongoService;

        public DataMergeService(ApiClient apiClient, MongoService mongoService)
        {
            _apiClient = apiClient;
            _mongoService = mongoService;
        }

        public async Task SaveFetchedDataAsync()
        {
            var endpoint1 = "https://arun-test.free.beeceptor.com";
            var endpoint2 = "https://arun-test.free.beeceptor.com";


            var weeklyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint1);
            var monthlyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint2);


            // Create a merged document with separate fields
            var mergedData = new MergedData
            {
                Weekly = weeklyResponse,
                Monthly = monthlyResponse
            };

            // Save to MongoDB
            await _mongoService.SaveDataAsync(mergedData);
        }
    }
}
