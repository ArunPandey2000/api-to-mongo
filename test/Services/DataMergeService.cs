using System.Collections.Generic;
using System.Transactions;
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
            string weeklyApiKey = Environment.GetEnvironmentVariable("WEEKLY_API_KEY");
            string feErrorsApiKey = Environment.GetEnvironmentVariable("FE_ERRORS_API_KEY");

            if (string.IsNullOrEmpty(weeklyApiKey) || string.IsNullOrEmpty(feErrorsApiKey))
            {
                throw new InvalidOperationException("API-Key is missing from the environment variables.");
            }

            var endpoint = "https://arun-test.free.beeceptor.com";

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

            string feQuery = @"
            {
              actor {
                account(id: 468142) {
                  nrql(query: ""SELECT HashPath, Sample as 'Sample (Need attention and required fix)', CSPContext, LineNumber, ColumnNumber, SourceFile, UserName, BlockedURI, ViolatedDirective, Location 
                          FROM Transaction 
                          WHERE appName LIKE '%uat%' 
                            AND HashPath LIKE '%task%' 
                            AND appName LIKE '%security-csp%' 
                            AND request.uri LIKE '%cspreport%' 
                          LIMIT 10 
                          SINCE 30 minutes ago"") {
                    embeddedChartUrl
                    nrql
                    otherResult
                    rawResponse
                    staticChartUrl
                    totalResult
                  }
                }
              }
            }";


            // Fetch data using GraphQL
            var weeklyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint, weeklyQuery, weeklyApiKey);
            var monthlyResponse = await _apiClient.GetDataAsync<ApiResponse>(endpoint, monthlyQuery, weeklyApiKey);
            var feErrorResponse = await _apiClient.GetDataAsync<FrontEndErrorsResponse>(endpoint, feQuery, feErrorsApiKey);

            // Create a merged document with separate fields
            var mergedData = new MergedData
            {
                Weekly = weeklyResponse,
                Monthly = monthlyResponse,
                FrontendErrors = feErrorResponse
            };

            // Save to MongoDB
            await _mongoService.SaveDataAsync(mergedData);
        }
    }
}
