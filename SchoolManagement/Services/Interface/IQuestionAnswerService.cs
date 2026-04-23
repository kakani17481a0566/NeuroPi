using SchoolManagement.ViewModel.QuestionAnswer;
using System.Collections.Generic;
using System.Drawing;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionAnswerService
    {
        string AddAnswers(QuestionAnswerVM questions);
        List<QuestionAnswerVM> GetAnswersByEmpid(string empid, int tenantId);
    }
}
