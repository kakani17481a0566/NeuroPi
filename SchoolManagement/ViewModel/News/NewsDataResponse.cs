using System.Text.Json.Serialization;

namespace SchoolManagement.ViewModel.News
{
    public class NewsDataResponse
    {
        [JsonPropertyName("results")]
        public List<NewsArticleApiResult> Results { get; set; }
    }

    public class NewsArticleApiResult
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PubDate { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
    }
}
