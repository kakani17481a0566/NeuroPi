namespace SchoolManagement.ViewModel.TimeTable
{
    /// <summary>
    /// Response ViewModel for returning eligible values to insert a TimeTable record.
    /// </summary>
    public class TimeTableInsertTableOptionsVM
    {
        public List<IdNameVM> Weeks { get; set; } = new();
        public List<IdNameVM> Holidays { get; set; } = new();
        public List<IdNameVM> Courses { get; set; } = new();
        public List<IdNameVM> Tenants { get; set; } = new();
        public List<CodeNameVM> AssessmentStatuses { get; set; } = new();
        public List<string> StatusOptions { get; set; } = new() { "working", "holiday" };
    }

    /// <summary>
    /// Generic ViewModel for ID-name pairs (used for weeks, holidays, courses, tenants).
    /// </summary>
    public class IdNameVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// ViewModel for master data with integer codes (like assessment status).
    /// </summary>
    public class CodeNameVM
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
