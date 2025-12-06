namespace SchoolManagement.ViewModel.TableFiles
{
    public class TimetableAttachmentUpdateVM
    {
        public int CourseRefId { get; set; }
        public int? TimeTableId { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public string? Type { get; set; }
        public int UpdatedBy { get; set; }
    }
}
