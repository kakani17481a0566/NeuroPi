using SchoolManagement.ViewModel.Utilites;

namespace SchoolManagement.Services.Interface
{
    public interface IUtilitesService
    {
        

        List<UtilitesResponseVM> GetAll(int tenantId);

       
    }
}
