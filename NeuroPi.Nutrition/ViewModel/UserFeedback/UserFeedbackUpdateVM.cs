using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFeedback
{
    public class UserFeedbackUpdateVM
    {
        public int FeedbackTypeId { get; set; }

        public string FeedbackText { get; set; }

        public DateOnly Date { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public void UpdateModel(MUserFeedback entity)
        {
            entity.FeedbackTypeId = FeedbackTypeId;
            entity.FeedbackText = FeedbackText;
            entity.Date = Date;
            entity.UpdatedBy = UpdatedBy;
            entity.UpdatedOn = UpdatedOn;
        }
    }
}
