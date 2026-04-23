using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionAnswer;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionAnswerImpl : IQuestionAnswerService
    {
        private readonly SchoolManagementDb context;
        public QuestionAnswerImpl(SchoolManagementDb _context)
        {
            context = _context;
        }
        public string AddAnswers(QuestionAnswerVM questions)
        {
             List<MQuestionAnswer> answers = new List<MQuestionAnswer>();
            foreach (var answer in questions.AnswerVM)
            {
                MQuestionAnswer answerAnswer = new MQuestionAnswer();
                answerAnswer.TenantId = questions.tenantId;
                answerAnswer.CreatedBy = questions.createdBy;
                answerAnswer.EmpId = questions.empId;
                answerAnswer.QuestionsId = answer.QuestionId;
                answerAnswer.Answer = answer.Answer;
                answerAnswer.CreatedOn = DateTime.UtcNow;
                answers.Add(answerAnswer);
            }
            context.QuestionAnswer.AddRange(answers);
           int count= context.SaveChanges();
            if(count> 0) 
                return "saved Answers";
            return "Some error occured";
           
        }
    }
}
