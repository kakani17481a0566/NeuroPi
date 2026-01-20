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

        public async Task<List<MarketTickerViewModel>> GetMarketTickerAsync()
        {
            // Using CoinGecko API - 100% free, no API key required
            string url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin,ethereum&vs_currencies=usd&include_24hr_change=true";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("CoinGecko API returned error: {StatusCode}", response.StatusCode);
                    return new List<MarketTickerViewModel>();
                }

                var apiData = await response.Content.ReadFromJsonAsync<CoinGeckoResponse>();

                if (apiData == null) return new List<MarketTickerViewModel>();

                var marketData = new List<MarketTickerViewModel>();

                // Bitcoin
                if (apiData.Bitcoin != null)
                {
                    marketData.Add(new MarketTickerViewModel
                    {
                        Symbol = "BTC",
                        Price = $"${apiData.Bitcoin.Usd:N0}",
                        Change = Math.Abs(apiData.Bitcoin.Usd24hChange).ToString("F2"),
                        Positive = apiData.Bitcoin.Usd24hChange >= 0
                    });
                }

                // Ethereum
                if (apiData.Ethereum != null)
                {
                    marketData.Add(new MarketTickerViewModel
                    {
                        Symbol = "ETH",
                        Price = $"${apiData.Ethereum.Usd:N0}",
                        Change = Math.Abs(apiData.Ethereum.Usd24hChange).ToString("F2"),
                        Positive = apiData.Ethereum.Usd24hChange >= 0
                    });
                }

                return marketData;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network error while fetching market data.");
                return new List<MarketTickerViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching market data.");
                return new List<MarketTickerViewModel>();
            }
        }

        public async Task<List<FlashUpdateViewModel>> GetFlashUpdatesAsync()
        {
            try
            {
                // Reuse existing financial news method
                var allNews = await GetFinancialNewsAsync();

                if (allNews == null || !allNews.Any())
                {
                    return new List<FlashUpdateViewModel>();
                }

                // Filter for recent news (last 2 hours)
                var twoHoursAgo = DateTime.Now.AddHours(-2);
                var recentNews = allNews
                    .Where(news => news.PublishedAt >= twoHoursAgo)
                    .OrderByDescending(news => news.PublishedAt)
                    .Take(3)
                    .ToList();

                // If no recent news, take the 3 most recent regardless of time
                if (!recentNews.Any())
                {
                    recentNews = allNews
                        .OrderByDescending(news => news.PublishedAt)
                        .Take(3)
                        .ToList();
                }

                // Map to FlashUpdateViewModel
                return recentNews.Select(news => new FlashUpdateViewModel
                {
                    Time = CalculateTimeAgo(news.PublishedAt),
                    Content = news.Title
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching flash updates.");
                return new List<FlashUpdateViewModel>();
            }
        }

        // Helper method to calculate time ago
        private string CalculateTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes}m ago";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours}h ago";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays}d ago";
            
            return dateTime.ToString("MMM dd");
        }
    }

    // Response models for CoinGecko API
    public class CoinGeckoResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("bitcoin")]
        public CoinData Bitcoin { get; set; }
        
        [System.Text.Json.Serialization.JsonPropertyName("ethereum")]
        public CoinData Ethereum { get; set; }
    }

    public class CoinData
    {
        [System.Text.Json.Serialization.JsonPropertyName("usd")]
        public decimal Usd { get; set; }
        
        [System.Text.Json.Serialization.JsonPropertyName("usd_24h_change")]
        public decimal Usd24hChange { get; set; }
    }
}
