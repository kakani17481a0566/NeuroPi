using SchoolManagement.ViewModel.CandidateCollege;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ICandidateCollegeService
    {
        List<CandidateCollegeResponseVM> GetByEmpIdAndTenantId(int empId, int tenantId);
    }
}