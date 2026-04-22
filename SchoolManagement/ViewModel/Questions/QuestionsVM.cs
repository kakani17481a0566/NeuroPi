namespace SchoolManagement.ViewModel.Questions
{
    public class QuestionsVM
    {
        public string QuestionType { get; set; }
        public List<Questions> Questions { get; set; }
       
    }
    public class Questions
    {
        public int orderId {  get; set; }
        public string Question { get; set; }

    }
}
