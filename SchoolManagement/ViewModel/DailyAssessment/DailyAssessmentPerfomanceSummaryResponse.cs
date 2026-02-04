public class DailyAssessmentPerformanceSummaryResponse
{
    public List<string> AssessmentStatusCode { get; set; } = new();
    public List<string> Headers { get; set; } = new();

    // ✅ Keyed by AssessmentId → StudentId → Score
    public Dictionary<int, Dictionary<int, AssessmentScoreVm>> AssessmentGrades { get; set; } = new();

    public List<StudentInfoVm> Students { get; set; } = new();
    public List<AssessmentHeaderVm> HeaderDetails { get; set; } = new();

    public List<SubjectGroupedAssessmentVm> SubjectWiseAssessments { get; set; } = new();

    public Dictionary<int, string> StudentDictionary { get; set; } = new();
    public Dictionary<int, string> SubjectDictionary { get; set; } = new();
    public Dictionary<int, string> WeekDictionary { get; set; } = new();
    public Dictionary<int, string> TermDictionary { get; set; } = new(); // ✅ NEW

    public string? WeekName { get; set; }
    public DateOnly? WeekStartDate { get; set; }
    public DateOnly? WeekEndDate { get; set; }

    public List<AssessmentScheduleVm> AssessmentSchedule { get; set; } = new();
    public List<WeeklyPerformanceVm> WeeklyAnalysis { get; set; } = new();
    public List<TermPerformanceVm> TermAnalysis { get; set; } = new(); // ✅ NEW
}

public class AssessmentScheduleVm
{
    public int TimeTableId { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
}

public class StudentInfoVm
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string StudentImageUrl { get; set; }
    public decimal? AverageScore { get; set; }
    public decimal? StandardDeviation { get; set; }
}

public class AssessmentScoreVm
{
    public string Grade { get; set; } = "Not Graded";
    public decimal? Score { get; set; }
    public DateTime? AssessmentDate { get; set; }
}

public class AssessmentHeaderVm
{
    public int AssessmentId { get; set; }
    public string Name { get; set; }
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string SubjectCode { get; set; }
}

public class SubjectGroupedAssessmentVm
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public string SubjectCode { get; set; }

    // ✅ NEW → for term or week aggregated view
    public string Grade { get; set; } = "Not Graded";
    public decimal? AverageScore { get; set; }

    public List<SkillAssessmentVm> Skills { get; set; } = new();
}

public class SkillAssessmentVm
{
    public int AssessmentId { get; set; }
    public string Name { get; set; }
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public List<StudentScoreEntryVm> StudentScores { get; set; } = new();
}

public class StudentScoreEntryVm
{
    public int StudentId { get; set; }
    public string Grade { get; set; }
    public decimal? Score { get; set; }
    public DateTime? AssessmentDate { get; set; }
}

public class WeeklyPerformanceVm
{
    public int WeekId { get; set; }
    public string WeekName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public decimal? AverageScore { get; set; }
    public decimal? StandardDeviation { get; set; }

    public List<SubjectGroupedAssessmentVm> SubjectWiseAssessments { get; set; } = new();
}

// ✅ NEW: Term-level aggregation
public class TermPerformanceVm
{
    public int TermId { get; set; }
    public string TermName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public decimal? AverageScore { get; set; }
    public decimal? StandardDeviation { get; set; }

    // ✅ NEW → overall grade for the term
    public string Grade { get; set; } = "Not Graded";

    public List<SubjectGroupedAssessmentVm> SubjectWiseAssessments { get; set; } = new();
}
