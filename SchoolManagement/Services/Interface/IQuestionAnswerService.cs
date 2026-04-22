using SchoolManagement.ViewModel.QuestionAnswer;
using System.Drawing;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionAnswerService
    {
        string AddAnswers(QuestionAnswerVM questions);
    }
}
