using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Week
{
    public class WeekVm
    {
        public int Id { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TenantId { get; set; }

        public static WeekVm FromModel(MWeek week)
        {
            if (week == null) return null;

            return new WeekVm
            {
                Id = week.Id,
                TermId = week.TermId,
                Name = week.Name,
                StartDate = week.StartDate,
                EndDate = week.EndDate,
                TenantId = week.TenantId
            };
        }
    }
}
