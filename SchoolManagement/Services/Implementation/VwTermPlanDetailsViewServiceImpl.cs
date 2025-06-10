using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VwTermPlanDetails;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class VwTermPlanDetailsViewServiceImpl : IVwTermPlanDetailsService
    {
        private readonly SchoolManagementDb _dbContext;

        public VwTermPlanDetailsViewServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<VwTermPlanDetailsViewModel> GetAllByTenantId(int tenantId)
        {
            return _dbContext.VwTermPlanDetails
                .Where(x => x.TenantId == tenantId)
                .Select(VwTermPlanDetailsViewModel.FromModel)
                .ToList();
        }
    }
}
