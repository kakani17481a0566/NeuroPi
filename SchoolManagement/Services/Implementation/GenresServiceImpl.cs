using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Genres;

namespace SchoolManagement.Services.Implementation
{
    public class GenresServiceImpl : IGenreService
    {
        private readonly SchoolManagementDb _context;
        public GenresServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public List<GenresResponseVM> GetAllGenres(int tenantId)
        {
            var result=_context.generes.Where(e=>!e.IsDeleted && e.TenantId == tenantId).ToList();
            if (result != null)
            {
                return GenresResponseVM.toViewModelList(result);
            }

                return [];
        }
    }
}
