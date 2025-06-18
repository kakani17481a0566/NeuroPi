using System.Collections.Generic;

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
        public Dictionary<string, string> Grades { get; set; } = new();
    }
}
