namespace SchoolManagement.ViewModel.QuestionPaper
{
    public class QuestionPaperUpdateVM
    {
        public string PaperName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
