namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class AssessmentMatrixResponse
    {
        public List<string> Headers { get; set; } = new();
        public List<AssessmentMatrixRow> Rows { get; set; } = new();
        public List<AssessmentStatusVm> AssessmentStatusCode { get; set; } = new();

        // ✅ Header (e.g. "PSED[A1]") to AssessmentId map for safe saving
        public Dictionary<string, int> HeaderSkillMap { get; set; } = new();

        public int CurrentStatusId { get; set; }
        public string CurrentStatusName { get; set; }
    }

    public class AssessmentMatrixRow
    {
        public int SerialNumber { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public Dictionary<string, GradeDetail> AssessmentGrades { get; set; } = new();
    }

    public class GradeDetail
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }

    public class AssessmentStatusVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
