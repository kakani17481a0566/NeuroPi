using SchoolManagement.ViewModel.Period;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IPeriodService
    {
        List<PeriodResponseVM> GetAll();
        List<PeriodResponseVM> GetByTenantId(int tenantId);
        PeriodResponseVM GetById(int id);
        PeriodResponseVM GetByIdAndTenantId(int id, int tenantId);
        PeriodResponseVM Create(PeriodRequestVM model);
        PeriodResponseVM Update(int id, int tenantId, PeriodUpdateVM model);
        bool Delete(int id, int tenantId);
    }
}
