using SchoolManagement.ViewModel.PublicHoliday;

namespace SchoolManagement.ViewModel.QuestionAnswer
{
    public class QuestionAnswerVM
    {
        public int tenantId { get; set; }
        public int createdBy { get; set; }
        public int empId { get; set; }
        public List<AnswerVM> AnswerVM { get; set; }
    }
    public class AnswerVM
    {
        public int QuestionId { get; set; }
        public int? QOrderId { get; set; }
        public string Qus { get; set; }

        public string Answer { get; set; }
    }
}
