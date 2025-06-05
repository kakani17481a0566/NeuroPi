using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.PublicHoliday
{
    public class PublicHolidayRequestVM
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public static MPublicHoliday ToModel(PublicHolidayRequestVM vm)
        {
            return new MPublicHoliday
            {
                Name = vm.Name,
                Date = vm.Date,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy
            };
        }
    }
}
