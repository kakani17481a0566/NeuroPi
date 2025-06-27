namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class SaveAttendanceRequestVm
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }

        // ✅ Instead of just StudentIds, pass entries with time info
        public List<AttendanceEntry> Entries { get; set; } = new();
    }

    public class AttendanceEntry
    {
        public int StudentId { get; set; }

        // FromTime is optional — for check-out only
        public TimeSpan FromTime { get; set; }

        // ToTime is optional — for check-in only
        public TimeSpan ToTime { get; set; }
    }
}
