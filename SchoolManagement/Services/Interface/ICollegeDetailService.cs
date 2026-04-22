using SchoolManagement.ViewModel.CollegeDetail;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICollegeDetailService
    {
        List<CollegeDetailResponseVM> GetAllByTenantId(int tenantId);
        List<ComprehensiveCollegeVM> GetAllComprehensiveByTenantId(int tenantId);
    }
}
