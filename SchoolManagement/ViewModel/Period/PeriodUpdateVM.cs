using System;

namespace SchoolManagement.ViewModel.Period
{
    public class PeriodUpdateVM
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int UpdatedBy { get; set; }
    }
}
