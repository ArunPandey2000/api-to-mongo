﻿using test.Models;

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

                var weeklyQuery = @"{
        actor {
            account(id: 468142) {
                nrql(
                    query: ""select count(*) from TransactionError where appName like 'Azure_PROD-Direct_US_Leo.Order.Lines-API' since monday until today FACET error.message limit 100""
                ) {
                    results
                }
            }
            user {
                name
            }
        }
    }";

                var monthlyQuery = @"
            {
        actor {
            account(id: 468142) {
                nrql(
                    query: ""select count(*) from TransactionError where appName like 'Azure_PROD-Direct_US_Leo.Order.Lines-API' since 1 month ago until today FACET error.message limit 100""
                ) {
                    results
                }
            }
            user {
                name
            }
        }
    }";

            // Fetch data using GraphQL
            var weeklyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint1, weeklyQuery);
            var monthlyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint2, monthlyQuery);

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
