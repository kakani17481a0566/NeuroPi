namespace SchoolManagement.ViewModel.TableFiles
{
    public class TimetableAttachmentCreateVM
    {
        public int CourseRefId { get; set; }
        public int? TimeTableId { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Type { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
    }
}
