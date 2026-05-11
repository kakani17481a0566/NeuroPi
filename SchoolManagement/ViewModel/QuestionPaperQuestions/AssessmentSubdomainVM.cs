namespace SchoolManagement.ViewModel.QuestionPaperQuestions
{
    public class AssessmentSubdomainVM
    {
        public string Domainname { get; set; }
        public string Subdomainname { get; set; }
        public List<AssessmentQuestionVM> Questions { get; set; }
    }
}
