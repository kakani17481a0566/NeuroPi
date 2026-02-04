using SchoolManagement.ViewModel.LibraryBookTitle;

namespace SchoolManagement.Services.Interface
{
    public interface ILibraryBookTitleService
    {
        List<LibraryBookTitleResponseVM> GetAll();
        LibraryBookTitleVM GetById(int id);
        LibraryBookTitleResponseVM CreateLibraryBookTitle(LibraryBookTitleRequestVM request); 
        LibraryBookTitleResponseVM UpdateLibraryBookTitle(int id, LibraryBookTitleRequestVM request); 
        bool DeleteLibraryBookTitle(int id); 
        List<LibraryBookTitleResponseVM> GetAllByTenantId(int tenantId); 
        LibraryBookTitleVM GetByIdAndTenantId(int id, int tenantId);
    }
}
