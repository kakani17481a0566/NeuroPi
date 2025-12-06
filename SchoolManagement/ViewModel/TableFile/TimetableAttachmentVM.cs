namespace SchoolManagement.ViewModel.TableFile
{
    public class TimetableAttachmentVM
    {
        public int Id { get; set; }

        public int CourseRefId { get; set; }     // Renamed CourseId → CourseRefId
        public int? TimeTableId { get; set; }

        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Type { get; set; }

        public int? TenantId { get; set; }
    }

}
