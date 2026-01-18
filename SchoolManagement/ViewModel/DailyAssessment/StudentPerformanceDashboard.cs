namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class StudentPerformanceDashboard
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string? ImageUrl { get; set; }

        public PerformanceBreakdown Daily { get; set; }
        public PerformanceBreakdown Weekly { get; set; }
        public PerformanceBreakdown Monthly { get; set; }
        public PerformanceBreakdown Yearly { get; set; }

        public List<AssessmentGroup> AssessmentWise { get; set; } = new();

        // KPIs
        public decimal? OverallAverageScore { get; set; }
        public decimal? ClassAverageScore { get; set; }
        public decimal? AttendancePercentage { get; set; }
        public int TotalAssessments { get; set; }
    }

    public class PerformanceBreakdown
    {
        public string Period { get; set; } // e.g., "2025-07-29", "Week 5", "July 2025"
        public List<AssessmentResult> Results { get; set; } = new();
    }

    public class AssessmentResult
    {
        public string AssessmentName { get; set; }
        public string SkillName { get; set; }
        public string SubjectName { get; set; }

        public string Grade { get; set; }
        public decimal? Score { get; set; }          // ✅ Add this line
        public DateTime Date { get; set; }           // ✅ Add this line
        public decimal? MinPercentage { get; set; }
        public decimal? MaxPercentage { get; set; }

        public string TimeTableDay { get; set; }
        public DateTime AssessmentDate { get; set; }
    }

    public class AssessmentGroup
    {
        public string AssessmentName { get; set; }
        public string SkillName { get; set; }
        public string SubjectName { get; set; }

        public string Grade { get; set; }
        public decimal? MinPercentage { get; set; }
        public decimal? MaxPercentage { get; set; }

        public string TimeTableDay { get; set; }
        public DateTime AssessmentDate { get; set; }
    }
}
