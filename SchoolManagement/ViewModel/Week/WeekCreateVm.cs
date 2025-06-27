using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Week
{
    public class WeekCreateVm
    {
        public int TermId { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public MWeek ToModel()
        {
            return new MWeek
            {
                TermId = this.TermId,
                Name = this.Name,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
