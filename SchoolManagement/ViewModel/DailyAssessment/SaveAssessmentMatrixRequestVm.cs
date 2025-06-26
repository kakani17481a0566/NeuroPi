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

        // ✅ Required now: status always comes from frontend
        public int OverrideStatusCode { get; set; }
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
