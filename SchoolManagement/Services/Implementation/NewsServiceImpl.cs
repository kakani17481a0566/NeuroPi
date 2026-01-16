using System.Net.Http.Json;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.News;

namespace SchoolManagement.Services.Implementation
{
    public class NewsServiceImpl : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsServiceImpl> _logger;

        public NewsServiceImpl(HttpClient httpClient, ILogger<NewsServiceImpl> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NewsViewModel>> GetFinancialNewsAsync()
        {
            string url = "https://newsdata.io/api/1/latest?apikey=pub_0afc27f8232041e5890cdbe409c84c68&q=Business%20financial&country=in&language=en,te";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Third-party API returned error: {StatusCode}", response.StatusCode);
                    return new List<NewsViewModel>();
                }

                // Deserializing raw API data
                var apiData = await response.Content.ReadFromJsonAsync<NewsDataResponse>();

                if (apiData?.Results == null) return new List<NewsViewModel>();

                // Mapping to ViewModel
                return apiData.Results.Select(item => new NewsViewModel
                {
                    Title = item.Title ?? "No Title",
                    Description = item.Description ?? "No description available.",
                    // PublishedAt is DateTime in our ViewModel, but string in API. Parsing it.
                    // If parsing fails, use current date.
                    PublishedAt = DateTime.TryParse(item.PubDate, out var date) ? date : DateTime.Now,
                    ImagePath = item.ImageUrl ?? "https://via.placeholder.com/300?text=No+Image"
                }).ToList();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error while fetching news.");
                return new List<NewsViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return new List<NewsViewModel>();
            }
        }
    }
}
