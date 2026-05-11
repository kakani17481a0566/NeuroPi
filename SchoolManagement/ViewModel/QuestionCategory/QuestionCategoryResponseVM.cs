using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionCategory
{
    public class QuestionCategoryResponseVM
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static QuestionCategoryResponseVM ToViewModel(MQuestionCategory questionCategory)
        {
            return new QuestionCategoryResponseVM
            {
                Id = questionCategory.Id,
                CategoryName = questionCategory.CategoryName,
                Description = questionCategory.Description,
                Code = questionCategory.Code,
                IsActive = questionCategory.IsActive,
                TenantId = questionCategory.TenantId,
                CreatedBy = questionCategory.CreatedBy,
                CreatedOn = questionCategory.CreatedOn,
                UpdatedBy = questionCategory.UpdatedBy,
                UpdatedOn = questionCategory.UpdatedOn
            };
        }

        public static List<QuestionCategoryResponseVM> ToViewModelList(List<MQuestionCategory> questionCategories)
        {
            return questionCategories.Select(q => ToViewModel(q)).ToList();
        }
    }
}
