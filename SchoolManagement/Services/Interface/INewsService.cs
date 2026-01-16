using SchoolManagement.ViewModel.News;

namespace SchoolManagement.Services.Interface
{
    public interface INewsService
    {
        Task<List<NewsViewModel>> GetFinancialNewsAsync();
    }
}
