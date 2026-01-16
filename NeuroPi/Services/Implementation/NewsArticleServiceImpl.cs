using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.NewsArticle;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class NewsArticleServiceImpl : INewsArticle
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsArticleServiceImpl> _logger;

        
        public NewsArticleServiceImpl(HttpClient httpClient, ILogger<NewsArticleServiceImpl> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NewsResponseVM>> GetFinancialNewsAsync()
        {
            string url = "https://newsdata.io/api/1/latest?apikey=pub_0afc27f8232041e5890cdbe409c84c68&q=Business%20financial&country=in&language=en,te";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Third-party API returned error: {StatusCode}", response.StatusCode);
                    return new List<NewsResponseVM>();
                }

                // Deserializing raw API data
                var apiData = await response.Content.ReadFromJsonAsync<NewsDataResponse>();

                if (apiData?.Results == null) return new List<NewsResponseVM>();

                // Mapping to your specific ViewModel: NewsResponseVM
                return apiData.Results.Select(item => new NewsResponseVM
                {
                    Title = item.Title ?? "No Title",
                    Description = item.Description ?? "No description available.",
                    PublishedAt = item.PubDate, 
                    ImagePath = item.ImageUrl ?? "https://via.placeholder.com/300?text=No+Image"
                }).ToList();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error while fetching news.");
                return new List<NewsResponseVM>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return new List<NewsResponseVM>();
            }
        }
    }
}