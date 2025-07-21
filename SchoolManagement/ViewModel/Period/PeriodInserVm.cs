using SchoolManagement.Model;

public class PeriodRequestVM
{
    public string Name { get; set; } = null!;
    public int CourseId { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int TenantId { get; set; }
    public int CreatedBy { get; set; }

    public MPeriod ToModel()
    {
        return new MPeriod
        {
            Name = Name,
            CourseId = CourseId,
            StartTime = StartTime,
            EndTime = EndTime,
            TenantId = TenantId,
            CreatedBy = CreatedBy,     // comes from MBaseModel
            CreatedOn = DateTime.UtcNow,
            IsDeleted = false
        };
    }
}
