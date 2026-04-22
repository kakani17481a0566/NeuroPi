using SchoolManagement.ViewModel.PublicHoliday;

namespace SchoolManagement.ViewModel.QuestionAnswer
{
    public class QuestionAnswerVM
    {
        public int tenantId { get; set; }
        public int createdBy { get; set; }
        public List<AnswerVM> AnswerVM { get; set; }
    }
    public class AnswerVM
    {
        public int QuestionId { get; set; }

        public string Answer { get; set; }
    }
}
