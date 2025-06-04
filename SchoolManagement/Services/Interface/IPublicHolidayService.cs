using SchoolManagement.ViewModel.PublicHoliday;

namespace SchoolManagement.Services.Interface
{
    public interface IPublicHolidayService
    {
        PublicHolidayResponseVM GetById(int id);

        List<PublicHolidayResponseVM> GetAll();

        List<PublicHolidayResponseVM> GetAllByTenantId(int tenantId);
        PublicHolidayResponseVM GetByIdAndTenantId(int id, int tenantId);

        PublicHolidayResponseVM CreatePublicHoliday(PublicHolidayRequestVM masterType);


        PublicHolidayResponseVM UpdatePublicHoliday(int id, int tenantId, PublicHolidayRequestVM masterType);

        PublicHolidayResponseVM DeleteById(int id, int tenantId);
    }
}
