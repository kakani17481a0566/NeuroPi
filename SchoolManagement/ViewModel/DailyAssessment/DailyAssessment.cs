namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class AssessmentMatrixResponse
    {
        public List<string> Headers { get; set; } = new();
        public List<AssessmentMatrixRow> Rows { get; set; } = new();
        public List<AssessmentStatusVm> AssessmentStatusCode { get; set; } // Changed from List<string>
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
