using SchoolManagement.ViewModel.News;

namespace SchoolManagement.Services.Interface
{
    public interface INewsService
    {
        Task<List<NewsViewModel>> GetFinancialNewsAsync();
        Task<List<MarketTickerViewModel>> GetMarketTickerAsync();
        Task<List<FlashUpdateViewModel>> GetFlashUpdatesAsync();
    }
}
