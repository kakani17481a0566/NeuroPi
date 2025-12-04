using SchoolManagement.ViewModel.Week;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IWeekService
    {

        List<WeekVm> GetAllWeeks();

        List<WeekVm> GetWeeksByTenantId(int tenantId);
        WeekVm GetWeekByIdAndTenantId(int id, int tenantId);
        WeekVm GetWeekById(int id);
        WeekVm CreateWeek(WeekCreateVm request);
        WeekVm UpdateWeek(int id, int tenantId, WeekUpdateVm request);

        WeekVm GetWeeksByTermAndTenant(int termId, int tenantId);    
        bool DeleteWeek(int id, int tenantId);
    }
}
