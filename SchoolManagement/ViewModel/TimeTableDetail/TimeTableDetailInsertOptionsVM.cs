using System.Collections.Generic;

namespace SchoolManagement.ViewModel.TimeTableDetail
{
    public class TimeTableDetailInsertOptionsVM
    {
        public List<CourseWithSubjectsVM> Courses { get; set; }
        public List<TimeTableDetailDropDownVM> Periods { get; set; }
        public List<TimeTableDetailDropDownVM> TimeTables { get; set; }
        public List<TimeTableDetailDropDownVM> Teachers { get; set; }
    }

    public class CourseWithSubjectsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SubjectWithWeeksVM> Subjects { get; set; }
    }

    public class SubjectWithWeeksVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TimeTableDetailDropDownVM> Weeks { get; set; }
    }

    public class TimeTableDetailDropDownVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add these for better filtering on frontend
        public int? CourseId { get; set; }
        public int? WeekId { get; set; }
        public int? PeriodId { get; set; }
    }
}
