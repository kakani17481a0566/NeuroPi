using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Questions;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionServiceImpl : IQuestionService
    {
        private readonly  SchoolManagementDb _context;
        public QuestionServiceImpl(SchoolManagementDb context)
        {
            _context = context;
            
        }
        public List<QuestionsVM> getAllQuestions(int tenantId)
        {
            var response=_context.Questions.Where(t=>t.TenantId == tenantId).Include(q=>q.QCtg).ToList();
            var result = response
                .GroupBy(x => new { x.QCtg.Id, x.QCtg.Name }) 
                .Select(group => new QuestionsVM
    {
                QuestionType = group.Key.Name, 

                Questions = group
                    .OrderBy(q => q.QOrderId)
                    .Select(q => new Questions
                    {
                         orderId = (int)q.QOrderId,
                         Question = q.Qus
                    })
                    .ToList()
                })
                .ToList();
            if (result != null && result.Count > 0)
            {
                return result;
            }
            return null;
        }
    }
}
