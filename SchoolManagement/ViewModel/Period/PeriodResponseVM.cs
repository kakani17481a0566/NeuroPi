using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Period
{
    public class PeriodResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }

        public static PeriodResponseVM FromModel(MPeriod model)
        {
            if (model == null) return null;

            return new PeriodResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                CourseId = model.CourseId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                TenantId = model.TenantId,
                CreatedOn = model.CreatedOn
            };
        }
    }
}
    