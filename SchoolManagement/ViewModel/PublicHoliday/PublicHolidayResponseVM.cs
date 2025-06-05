using SchoolManagement.Model;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.ViewModel.PublicHoliday
{
    public class PublicHolidayResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int TenantId { get; set; }

        public static PublicHolidayResponseVM ToViewModel(MPublicHoliday model)
        {
            if (model == null) return null;

            return new PublicHolidayResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Date = model.Date,
                TenantId = model.TenantId
            };
        }

        public static List<PublicHolidayResponseVM> ToViewModelList(List<MPublicHoliday> models)
        {
            if (models == null || models.Count == 0) return null;

            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
