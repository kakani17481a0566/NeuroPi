using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionCategory
{
    public class QuestionCategoryRequestVM
    {
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MQuestionCategory ToModel(QuestionCategoryRequestVM request)
        {
            return new MQuestionCategory
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                Code = request.Code,
                IsActive = request.IsActive,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };
        }   
    }
}
