using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Utilites;

namespace SchoolManagement.Services.Implementation
{
    public class UtilitiesServiceImpl : IUtilitesService
    {
        private readonly SchoolManagementDb _context;
        public UtilitiesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public List<UtilitesResponseVM> GetAll(int tenantId)
        {
            var result = _context.UtilitiesList.Where(u => !u.IsDeleted).ToList();
            if (result != null)
            {
                return UtilitesResponseVM.ToViewModelList(result);
            }
            return null;
        }
    }
}
