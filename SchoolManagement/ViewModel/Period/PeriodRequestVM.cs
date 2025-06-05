using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.Period
{
    public class PeriodRequestVM
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public MPeriod ToModel()
        {
            return new MPeriod
            {
                Name = Name,
                CourseId = CourseId,
                StartTime = StartTime,
                EndTime = EndTime,
                TenantId = TenantId,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
