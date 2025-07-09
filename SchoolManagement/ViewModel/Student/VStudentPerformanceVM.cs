namespace SchoolManagement.ViewModel.Student
{
    public class VStudentPerformanceVM
    {
        public int AssessmentId { get; set; }
        public DateTime AssessmentDate { get; set; }

        // Student Info
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        // Grade Info
        public int? GradeId { get; set; }
        public string Grade { get; set; }
        public string GradeDescription { get; set; }

        // Timetable Info
        public int TimeTableId { get; set; }
        public string DayName { get; set; }
        public DateTime TimeTableDate { get; set; }

        // Week Info
        public int? WeekId { get; set; }
        public string WeekName { get; set; }
        public DateTime? WeekStartDate { get; set; }
        public DateTime? WeekEndDate { get; set; }

        // Term Info
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }

        // Course Info
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        // Assessment Item Info
        public int AssessmentItemId { get; set; }
        public string AssessmentName { get; set; }

        // Skill Info
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
    }
}
