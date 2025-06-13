using NeuroPi.UserManagment.ViewModel.Branch;
using System.Collections.Generic;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IBranchService
    {
        List<BranchResponseVM> GetAllBranches(int tenantId);
    }
}
