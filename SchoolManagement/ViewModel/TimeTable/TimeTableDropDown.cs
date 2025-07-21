using System.Collections.Generic;

namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableDropDown
    {
        public Dictionary<int, CourseInfo> Courses { get; set; } = new();

        // ✅ Renamed to avoid conflict
        public List<TopicTypeOptionVM> TopicTypes { get; set; } = new();
    }

    public class CourseInfo
    {
        public string Name { get; set; }
        public Dictionary<int, string> Subjects { get; set; } = new();
    }

    // ✅ New, specific ViewModel for topic type dropdown
    public class TopicTypeOptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
