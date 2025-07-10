public class DailyAssessmentPerformanceSummaryResponse
{
    public List<string> AssessmentStatusCode { get; set; } = new();
    public List<string> Headers { get; set; } = new();
    public Dictionary<string, Dictionary<int, AssessmentScoreVm>> AssessmentGrades { get; set; } = new();
    public List<StudentInfoVm> Students { get; set; } = new();
}

public class StudentInfoVm
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public decimal? AverageScore { get; set; }
    public decimal? StandardDeviation { get; set; } // ✅ NEW

}

public class AssessmentScoreVm // <- Renamed from AssessmentGradeVm
{
    public string Grade { get; set; } = "Not Graded";
    public decimal? Score { get; set; } = null;
    public DateTime? AssessmentDate { get; set; } // ✅ NEW

}
