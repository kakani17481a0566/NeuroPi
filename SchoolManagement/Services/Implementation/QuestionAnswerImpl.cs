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
                .Where(x => x.EmpId == empIdInt && x.TenantId == tenantId && !x.IsDeleted)
                .ToList();

            var questions = context.Questions
                .Where(q => q.TenantId == tenantId && !q.IsDeleted && q.QCtg != null && !q.QCtg.IsDeleted)
                .OrderBy(q => q.QOrderId)
                .Select(q => new { q, QCtgName = q.QCtg.Name })
                .ToList();

            var result = new List<QuestionAnswerVM>();
            foreach (var item in questions)
            {
                var question = item.q;
                var answer = answers.FirstOrDefault(a => a.QuestionsId == question.Id);
                var vm = new QuestionAnswerVM();
                vm.empId = empIdInt;
                vm.tenantId = tenantId;
                vm.createdBy = answer?.CreatedBy ?? 0;
                vm.AnswerVM = new List<AnswerVM>
                {
                    new AnswerVM
                    {
                        QuestionId = question.Id,
                        QOrderId = question.QOrderId,
                        Qus = question.Qus,
                        QCtgName = item.QCtgName,
                        Answer = answer?.Answer,
                        IsAnswered = answer != null
                    }
                };
                result.Add(vm);
            }
            return result;
        }
    }
}
