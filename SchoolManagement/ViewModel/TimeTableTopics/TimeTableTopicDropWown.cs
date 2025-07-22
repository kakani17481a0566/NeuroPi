namespace SchoolManagement.ViewModel.TimeTableTopics
{
    public class TopicDropdownVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TimeTableDetailDropdownVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubjectDropdownVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TopicDropdownVM> Topics { get; set; } = new();
        public List<TimeTableDetailDropdownVM> TimeTableDetails { get; set; } = new();
    }

    public class CourseDropdownVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectDropdownVM> Subjects { get; set; } = new();
    }

    // This is your overall VM for API response
    public class TimeTableTopicDropdown
    {
        public List<CourseDropdownVM> Courses { get; set; } = new();
    }
}
