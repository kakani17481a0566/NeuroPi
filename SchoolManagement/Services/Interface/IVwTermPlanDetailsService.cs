using SchoolManagement.ViewModel.VwTermPlanDetails;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IVwTermPlanDetailsService
    {
        List<VwTermPlanDetailsViewModel> GetAllByTenantId(int tenantId);
    }
}
