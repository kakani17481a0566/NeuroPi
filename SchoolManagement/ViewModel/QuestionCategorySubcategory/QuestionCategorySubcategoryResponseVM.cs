using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionCategorySubcategory
{
    public class QuestionCategorySubcategoryResponseVM
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string SubcategoryName { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static QuestionCategorySubcategoryResponseVM ToViewModel(MQuestionCategorySubcategory categorySubcategory)
        {
            return new QuestionCategorySubcategoryResponseVM
            {
                Id = categorySubcategory.Id,
                CategoryId = categorySubcategory.CategoryId,
                SubcategoryName = categorySubcategory.SubcategoryName,
                Code = categorySubcategory.Code,
                Description = categorySubcategory.Description,
                IsActive = categorySubcategory.IsActive,
                TenantId = categorySubcategory.TenantId,
                CreatedBy = categorySubcategory.CreatedBy,
                CreatedOn = categorySubcategory.CreatedOn,
                UpdatedBy = categorySubcategory.UpdatedBy,
                UpdatedOn = categorySubcategory.UpdatedOn,


            };
        }

        public static List<QuestionCategorySubcategoryResponseVM> ToViewModelList(List<MQuestionCategorySubcategory> categorySubcategory)
        {
            return categorySubcategory.Select(x => ToViewModel(x)).ToList();
        }
    }
}