namespace SchoolManagement.ViewModel.QuestionCategorySubcategory
{
    public class QuestionCategorySubcategoryUpdateVM
    {
        public int CategoryId { get; set; }

        public string SubcategoryName { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
