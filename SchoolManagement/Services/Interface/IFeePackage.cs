using SchoolManagement.ViewModel.FeePackage;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IFeePackage
    {
        List<FeePackageResponseVM> GetAll(int tenantId, int branchId);
        FeePackageResponseVM GetById(int id, int tenantId, int branchId);
        int Create(FeePackageRequestVM vm, int currentUserId);
        bool Update(int id, FeePackageRequestVM vm, int currentUserId);
        bool Delete(int id, int tenantId, int branchId, int currentUserId);
        List<FeePackageListVM> GetPackageList(int tenantId, int branchId);
        List<FeePackageGroupVM> GetGroupedPackages(int tenantId, int branchId);
    }

}
