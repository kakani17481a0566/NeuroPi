using SchoolManagement.Model;
using SchoolManagement.ViewModel.Master;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.PublicHoliday
{
    public class PublicHolidayRequestVM
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }

        public int tenantId { get; set; }

        public int CreatedBy { get; set; }

        public static MPublicHoliday ToModel(PublicHolidayRequestVM publicHolidayRequestVM)
        {
            return new MPublicHoliday
            {
               Name= publicHolidayRequestVM.Name,
               Date= publicHolidayRequestVM.Date,
               TenantId=publicHolidayRequestVM.tenantId,

            };
        }
    }
}
