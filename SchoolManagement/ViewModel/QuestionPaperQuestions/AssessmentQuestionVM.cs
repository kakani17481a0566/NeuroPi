namespace SchoolManagement.ViewModel.QuestionPaperQuestions
{
    public class AssessmentQuestionVM
    {
        public int Id { get; set; }
        public string Qid { get; set; }
        public string Qname { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public List<string> Qoptions { get; set; }
    }
}
