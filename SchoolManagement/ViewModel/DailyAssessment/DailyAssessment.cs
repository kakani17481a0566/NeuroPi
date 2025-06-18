namespace SchoolManagement.ViewModel.DailyAssessment
{
    public class AssessmentMatrixResponse
    {
        public List<string> Headers { get; set; } = new();
        public List<AssessmentMatrixRow> Rows { get; set; } = new();
    }

    public class AssessmentMatrixRow
    {
        public int SNo { get; set; }
        public int StudentId { get; set; }
        public string Name { get; set; }

        // Each key is the assessment name, value is grade detail with id
        public Dictionary<string, GradeDetail> Grades { get; set; } = new();
    }

    public class GradeDetail
    {
        public string Grade { get; set; }
        public int Id { get; set; }
    }
}
