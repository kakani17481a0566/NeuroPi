namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class SaveAssessmentMatrixRequestVm
    {
        public int TimeTableId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int ConductedById { get; set; }
        public int CourseId { get; set; }

        public List<StudentAssessmentGradeVm> Students { get; set; } = new();

        // ✅ Optional: allow user to override status manually
        public int? OverrideStatusCode { get; set; }

        // ✅ Optional: allow user to choose predefined logic mode
        public string? StatusLogicMode { get; set; } // "auto", "forceComplete", "forceInProgress"
    }

    public class StudentAssessmentGradeVm
    {
        public int StudentId { get; set; }
        public List<AssessmentGradeVm> Grades { get; set; } = new();
    }

    public class AssessmentGradeVm
    {
        public int AssessmentId { get; set; }
        public int GradeId { get; set; }
    }
}
