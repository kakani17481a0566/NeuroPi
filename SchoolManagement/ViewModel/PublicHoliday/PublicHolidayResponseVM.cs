using SchoolManagement.Model;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.ViewModel.PublicHoliday
{
    public class PublicHolidayResponseVM
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public DateOnly Date { get; set; }

        public int TenantId { get; set; }

        public static PublicHolidayResponseVM ToViewModel(MPublicHoliday publicHoliday) =>
            new PublicHolidayResponseVM
            {
                Id=publicHoliday.Id,
                Name=publicHoliday.Name,
                Date=publicHoliday.Date,
                TenantId=publicHoliday.TenantId,
                
            };
        public static List<PublicHolidayResponseVM> ToViewModelList(List<MPublicHoliday> masters)
        {
            return masters.Select(ToViewModel).ToList();
        }
    }
}
