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
        public List<QuestionAnswerVM> GetAnswersByEmpid(string empid)
        {
            int empIdInt = Convert.ToInt32(empid);
            var answers = context.QuestionAnswer.Where(x => x.EmpId == empIdInt).ToList();
            List<QuestionAnswerVM> result = new List<QuestionAnswerVM>();
            foreach(var answer in answers)
            {
                QuestionAnswerVM vm = new QuestionAnswerVM();
                vm.empId = (int)answer.EmpId;
                vm.tenantId = (int)answer.TenantId;
                vm.createdBy = (int)answer.CreatedBy;
                List<AnswerVM> answerList = new List<AnswerVM>();
                AnswerVM ans = new AnswerVM();
                ans.QuestionId = (int)answer.QuestionsId;
                ans.Answer = answer.Answer;
                answerList.Add(ans);
                vm.AnswerVM = answerList;
                result.Add(vm);
            }
            return result;
        }
    }
}
