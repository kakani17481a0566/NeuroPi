namespace NeuroPi.Nutrition.ViewModel.UserFeedback
{
    public class SaveFeedbackVM
    {
        public int FeedbackTypeId { get; set; }
        public string FeedbackText { get; set; }

        public string Date { get; set; } // accept from frontend
    }

}
