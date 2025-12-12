namespace SchoolManagement.ViewModel.Worksheets
{
    public class WorksheetRequestVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; } // URL to the uploaded file (e.g., Cloudinary URL)

        public int TenantId { get; set; }
        public int? SubjectId { get; set; }
        public int CreatedBy { get; set; }
    }
}
