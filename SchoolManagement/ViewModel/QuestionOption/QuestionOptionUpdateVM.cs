namespace SchoolManagement.ViewModel.QuestionOption
{
    public class QuestionOptionUpdateVM
    {
        public int QuestionId { get; set; }

        public int Sq { get; set; }

        public string? OptionCode { get; set; }

        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        
    }
}