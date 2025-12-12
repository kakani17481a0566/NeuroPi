namespace SchoolManagement.ViewModel.Worksheets
{
    public class WorksheetUpdateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; } // URL to the uploaded file (optional for updates)
        public int? SubjectId { get; set; }
        public int UpdatedBy { get; set; }
    }

}
