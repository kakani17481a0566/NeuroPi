using SchoolManagement.ViewModel.Branch;

namespace SchoolManagement.Services.Interface
{
    public interface IBranchService
    {
        List<BranchResponseVM> GetAllBranches();

        List<BranchResponseVM> GetBranchesByTenantId(int tenantId);

        BranchResponseVM GetBranchById(int id);

        BranchResponseVM GetBranchByIdAndTenantId(int id, int tenantId);

        BranchResponseVM AddBranch(BranchRequestVM branch);

        BranchResponseVM UpdateBranch(int id, int tenantId, BranchUpdateVM branch);

        bool DeleteBranch(int id,int tenantId);

        BranchResponseVM GetBranchByDepartmentId(int departmentId);
    }
}
