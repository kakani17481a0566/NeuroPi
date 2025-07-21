using SchoolManagement.Model;
using System.Collections.Generic;

namespace SchoolManagement.ViewModel.Period
{
    public class PeriodDataVmRaw
    {
        public Dictionary<string, string> Headers { get; set; } = new();
        public List<PeriodDisplayVmRaw> Data { get; set; } = new();
        public FilterDataVm FilterData { get; set; } = new(); // ✅ Added
    }

    public class PeriodDisplayVmRaw
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TenantName { get; set; }

        public static PeriodDisplayVmRaw FromModel(MPeriod model)
        {
            return new PeriodDisplayVmRaw
            {
                Id = model.Id,
                Name = model.Name,
                CourseName = model.Course?.Name ?? "",
                StartTime = model.StartTime.ToString(@"hh\:mm"),
                EndTime = model.EndTime.ToString(@"hh\:mm"),
                TenantName = model.Tenant?.Name ?? ""
            };
        }
    }

    public class FilterDataVm
    {
        public List<CourseVm> Courses { get; set; } = new();
    }

    public class CourseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
