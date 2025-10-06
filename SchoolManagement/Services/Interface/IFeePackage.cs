using SchoolManagement.ViewModel.FeePackage;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IFeePackage
    {
        // 🔹 Get all fee packages for a tenant + branch
        List<FeePackageResponseVM> GetAll(int tenantId, int branchId);

        // 🔹 Get a single package by Id (with branch validation)
        FeePackageResponseVM GetById(int id, int tenantId, int branchId);

        // 🔹 Create a new package
        int Create(FeePackageRequestVM vm);

        // 🔹 Update an existing package
        bool Update(int id, FeePackageRequestVM vm);

        // 🔹 Delete (soft delete usually)
        bool Delete(int id, int tenantId, int branchId);
    }
}
