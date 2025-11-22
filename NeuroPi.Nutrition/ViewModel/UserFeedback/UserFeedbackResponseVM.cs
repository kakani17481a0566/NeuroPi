using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFeedback
{
    public class UserFeedbackResponseVM
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int FeedbackTypeId { get; set; }
        public string FeedbackTypeName { get; set; }
        public string FeedbackText { get; set; }
        public DateOnly Date { get; set; }

        public int? TenantId { get; set; }       // FIXED: now nullable

        public int? CreatedBy { get; set; }      // FIXED: now nullable
        public DateTime? CreatedOn { get; set; } // FIXED: now nullable

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static UserFeedbackResponseVM ToViewModel(MUserFeedback model)
        {
            if (model == null) return null;

            return new UserFeedbackResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                FeedbackTypeId = model.FeedbackTypeId,
                FeedbackTypeName = model.FeedbackType?.Name,
                FeedbackText = model.FeedbackText,
                Date = model.Date,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<UserFeedbackResponseVM> ToViewModelList(List<MUserFeedback> models)
        {
            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
