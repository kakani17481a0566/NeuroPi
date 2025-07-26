namespace SchoolManagement.ViewModel.Worksheets
{
    public class WorksheetUpdateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; } // for file update
        public int? SubjectId { get; set; }
        public int UpdatedBy { get; set; }
    }

}
