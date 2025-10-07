using SchoolManagement.ViewModel.FeePackage;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IFeePackageMaster
    {
        List<FeePackageMasterResponseVM> GetAll(int tenantId, int branchId);
        FeePackageMasterResponseVM GetById(int id, int tenantId, int branchId);
        int Create(FeePackageMasterRequestVM vm, int currentUserId);
        bool Update(int id, FeePackageMasterRequestVM vm, int currentUserId);
        bool Delete(int id, int tenantId, int branchId, int currentUserId);
    }
}
