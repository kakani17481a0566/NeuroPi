using SchoolManagement.ViewModel.PublicHoliday;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IPublicHolidayService
    {
        List<PublicHolidayResponseVM> GetAll();
        List<PublicHolidayResponseVM> GetAllByTenantId(int tenantId);
        PublicHolidayResponseVM GetById(int id);
        PublicHolidayResponseVM GetByIdAndTenantId(int id, int tenantId);
        PublicHolidayResponseVM CreatePublicHoliday(PublicHolidayRequestVM request);
        PublicHolidayResponseVM UpdatePublicHoliday(int id, int tenantId, PublicHolidayRequestVM request);
        PublicHolidayResponseVM DeleteById(int id, int tenantId);
    }
}
