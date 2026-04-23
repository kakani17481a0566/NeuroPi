using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionAnswer;
using System.Collections.Generic;

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
        public List<QuestionAnswerVM> GetAnswersByEmpid(string empid, int tenantId)
        {
            int empIdInt = Convert.ToInt32(empid);
            var answers = context.QuestionAnswer
                .Where(x => x.EmpId == empIdInt && x.TenantId == tenantId)
                .Select(answer => new
                {
                    Answer = answer,
                    Question = answer.Question
                })
                .ToList();
            List<QuestionAnswerVM> result = new List<QuestionAnswerVM>();
            foreach(var item in answers)
            {
                QuestionAnswerVM vm = new QuestionAnswerVM();
                vm.empId = (int)item.Answer.EmpId;
                vm.tenantId = (int)item.Answer.TenantId;
                vm.createdBy = (int)item.Answer.CreatedBy;
                List<AnswerVM> answerList = new List<AnswerVM>();
                AnswerVM ans = new AnswerVM();
                ans.QuestionId = (int)item.Answer.QuestionsId;
                ans.QOrderId = item.Question?.QOrderId;
                ans.Qus = item.Question?.Qus;
                ans.Answer = item.Answer.Answer;
                answerList.Add(ans);
                vm.AnswerVM = answerList;
                result.Add(vm);
            }
            return result;
        }
    }
}
