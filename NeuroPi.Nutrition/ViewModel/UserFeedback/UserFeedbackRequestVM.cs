using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFeedback
{
    public class UserFeedbackRequestVM
    {
        public int UserId { get; set; }
        public int FeedbackTypeId { get; set; }
        public string FeedbackText { get; set; }
        public DateOnly Date { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public static MUserFeedback ToModel(UserFeedbackRequestVM vm)
        {
            return new MUserFeedback
            {
                UserId = vm.UserId,
                FeedbackTypeId = vm.FeedbackTypeId,
                FeedbackText = vm.FeedbackText,
                Date = vm.Date,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn
            };
        }
    }
}
