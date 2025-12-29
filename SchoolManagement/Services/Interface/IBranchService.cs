using SchoolManagement.ViewModel.Branch;

namespace SchoolManagement.Services.Interface
{
    public interface IBranchService
    {
        List<BranchVM> GetBranchesByTenantId(int tenantId);
        BranchVM GetBranchById(int id, int tenantId);
    }
}
