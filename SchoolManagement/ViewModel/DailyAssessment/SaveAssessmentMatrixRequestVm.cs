namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class SaveAssessmentMatrixRequestVm
    {
        public int TimeTableId { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int ConductedById { get; set; }

        public int assessmentCode { get; set; }

        public List<StudentAssessmentGradeVm> Students { get; set; } = new();
    }

    public class StudentAssessmentGradeVm
    {
        public int StudentId { get; set; }
        public List<AssessmentGradeVm> Grades { get; set; } = new();
    }

    public class AssessmentGradeVm
    {
        public int AssessmentId { get; set; } // ✅ REQUIRED for database save
        public int GradeId { get; set; }
    }
}
