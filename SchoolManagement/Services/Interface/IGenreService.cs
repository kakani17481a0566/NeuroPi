using SchoolManagement.ViewModel.Genres;

namespace SchoolManagement.Services.Interface
{
    public interface IGenreService
    {

        List<GenresResponseVM> GetAllGenres(int tenantId);
    }
}
