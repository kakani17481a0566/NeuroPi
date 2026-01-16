using NeuroPi.UserManagment.ViewModel.NewsArticle;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface INewsArticle
    {
        Task<List<NewsResponseVM>> GetFinancialNewsAsync();
    }
}
