namespace SchoolManagement.ViewModel.QuestionCategory
{
    public class QuestionCategoryUpdateVM
    {
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
