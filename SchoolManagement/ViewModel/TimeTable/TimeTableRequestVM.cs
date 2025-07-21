using SchoolManagement.Model;

public class TimeTableRequestVM
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int? WeekId { get; set; }
    public int? HolidayId { get; set; }
    public string Status { get; set; }
    public int? CourseId { get; set; }
    public int? AssessmentStatusCode { get; set; }
    public int TenantId { get; set; }
    public int CreatedBy { get; set; }

    public MTimeTable ToModel()
    {
        return new MTimeTable
        {
            Name = Name,
            Date = Date,
            WeekId = WeekId,
            HolidayId = HolidayId == 0 ? null : HolidayId,

            Status = Status,
            CourseId = CourseId,
            AssessmentStatusCode = AssessmentStatusCode,
            TenantId = TenantId,
            CreatedBy = CreatedBy,
            CreatedOn = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}
