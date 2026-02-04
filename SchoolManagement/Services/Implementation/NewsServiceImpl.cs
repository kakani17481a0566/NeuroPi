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
            var marketData = new List<MarketTickerViewModel>();

            try
            {
                // Yahoo Finance API - Free, no API key required
                // Including Indian market indices and stocks
                var symbols = new[] 
                { 
                    "^NSEI",           // NIFTY 50
                    "^BSESN",          // SENSEX
                    "RELIANCE.NS",     // Reliance Industries
                    "TCS.NS",          // TCS
                    "INFY.NS",         // Infosys
                    "BTC-USD",         // Bitcoin
                    "ETH-USD"          // Ethereum
                };

                foreach (var symbol in symbols)
                {
                    try
                    {
                        string url = $"https://query1.finance.yahoo.com/v8/finance/chart/{symbol}?interval=1d&range=2d";
                        var response = await _httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonData = await response.Content.ReadFromJsonAsync<YahooFinanceResponse>();

                            if (jsonData?.Chart?.Result != null && jsonData.Chart.Result.Any())
                            {
                                var result = jsonData.Chart.Result[0];
                                var meta = result.Meta;
                                var quote = result.Indicators?.Quote?.FirstOrDefault();

                                if (meta != null && quote != null)
                                {
                                    var currentPrice = meta.RegularMarketPrice;
                                    var previousClose = meta.PreviousClose;
                                    var change = ((currentPrice - previousClose) / previousClose) * 100;

                                    // Format symbol for display
                                    var displaySymbol = symbol switch
                                    {
                                        "^NSEI" => "NIFTY 50",
                                        "^BSESN" => "SENSEX",
                                        "RELIANCE.NS" => "RELIANCE",
                                        "TCS.NS" => "TCS",
                                        "INFY.NS" => "INFOSYS",
                                        "BTC-USD" => "BTC",
                                        "ETH-USD" => "ETH",
                                        _ => symbol.Replace(".NS", "").Replace("-USD", "")
                                    };

                                    // Format price based on symbol type
                                    var priceFormat = symbol.Contains("USD") ? $"${currentPrice:N0}" : $"₹{currentPrice:N2}";

                                    marketData.Add(new MarketTickerViewModel
                                    {
                                        Symbol = displaySymbol,
                                        Price = priceFormat,
                                        Change = Math.Abs(change).ToString("F2"),
                                        Positive = change >= 0
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to fetch data for {Symbol}", symbol);
                    }
                }

                // If we got data from API, return it
                if (marketData.Any())
                {
                    _logger.LogInformation("Successfully fetched {Count} market ticker items from Yahoo Finance", marketData.Count);
                    return marketData;
                }

                // Otherwise use fallback
                _logger.LogWarning("Yahoo Finance API returned no data, using fallback");
                return GetFallbackMarketData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching market data, using fallback");
                return GetFallbackMarketData();
            }
        }

        // Fallback market data when API is unavailable
        private List<MarketTickerViewModel> GetFallbackMarketData()
        {
            return new List<MarketTickerViewModel>
            {
                new MarketTickerViewModel
                {
                    Symbol = "NIFTY 50",
                    Price = "₹23,645.50",
                    Change = "0.85",
                    Positive = true
                },
                new MarketTickerViewModel
                {
                    Symbol = "SENSEX",
                    Price = "₹78,234.25",
                    Change = "0.72",
                    Positive = true
                },
                new MarketTickerViewModel
                {
                    Symbol = "BTC",
                    Price = "$95,234",
                    Change = "2.45",
                    Positive = true
                },
                new MarketTickerViewModel
                {
                    Symbol = "ETH",
                    Price = "$3,456",
                    Change = "1.82",
                    Positive = true
                }
            };
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

    // Response models for Yahoo Finance API
    public class YahooFinanceResponse
    {
        [System.Text.Json.Serialization.JsonPropertyName("chart")]
        public YahooChart Chart { get; set; }
    }

    public class YahooChart
    {
        [System.Text.Json.Serialization.JsonPropertyName("result")]
        public List<YahooResult> Result { get; set; }
    }

    public class YahooResult
    {
        [System.Text.Json.Serialization.JsonPropertyName("meta")]
        public YahooMeta Meta { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("indicators")]
        public YahooIndicators Indicators { get; set; }
    }

    public class YahooMeta
    {
        [System.Text.Json.Serialization.JsonPropertyName("regularMarketPrice")]
        public decimal RegularMarketPrice { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("previousClose")]
        public decimal PreviousClose { get; set; }
    }

    public class YahooIndicators
    {
        [System.Text.Json.Serialization.JsonPropertyName("quote")]
        public List<YahooQuote> Quote { get; set; }
    }

    public class YahooQuote
    {
        [System.Text.Json.Serialization.JsonPropertyName("close")]
        public List<decimal?> Close { get; set; }
    }
}
