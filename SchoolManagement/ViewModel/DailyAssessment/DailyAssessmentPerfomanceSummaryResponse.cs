public class DailyAssessmentPerformanceSummaryResponse
{
    public List<string> AssessmentStatusCode { get; set; } = new();
    public List<string> Headers { get; set; } = new();
    public Dictionary<string, Dictionary<int, AssessmentScoreVm>> AssessmentGrades { get; set; } = new();
    public List<StudentInfoVm> Students { get; set; } = new();
    public List<AssessmentHeaderVm> HeaderDetails { get; set; } = new();

    // ✅ Grouped by Subject → Skills → Student Scores (overall)
    public List<SubjectGroupedAssessmentVm> SubjectWiseAssessments { get; set; } = new();

    public Dictionary<int, string> StudentDictionary { get; set; } = new();
    public Dictionary<int, string> SubjectDictionary { get; set; } = new();
    public Dictionary<int, string> WeekDictionary { get; set; } = new();

    // ✅ Single Week info (legacy)
    public string? WeekName { get; set; }
    public DateOnly? WeekStartDate { get; set; }
    public DateOnly? WeekEndDate { get; set; }

    // ✅ Timetable schedule
    public List<AssessmentScheduleVm> AssessmentSchedule { get; set; } = new();

    // ✅ NEW: Week-wise analysis
    public List<WeeklyPerformanceVm> WeeklyAnalysis { get; set; } = new();
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

// ✅ NEW: Week-wise breakdown
public class WeeklyPerformanceVm
{
    public int WeekId { get; set; }
    public string WeekName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public decimal? AverageScore { get; set; }
    public decimal? StandardDeviation { get; set; }

    // Grouped Subject → Skill → Scores for that week
    public List<SubjectGroupedAssessmentVm> SubjectWiseAssessments { get; set; } = new();
}
