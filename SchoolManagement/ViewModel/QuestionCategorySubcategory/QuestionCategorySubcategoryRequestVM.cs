using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionCategorySubcategory
{
    public class QuestionCategorySubcategoryRequestVM
    {
        public int CategoryId { get; set; }

        public string SubcategoryName { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MQuestionCategorySubcategory ToModel(QuestionCategorySubcategoryRequestVM request)
        {
            return new MQuestionCategorySubcategory
            {
                CategoryId = request.CategoryId,
                SubcategoryName = request.SubcategoryName,
                Code = request.Code,
                Description = request.Description,
                IsActive = request.IsActive,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };
        }
    }
}
