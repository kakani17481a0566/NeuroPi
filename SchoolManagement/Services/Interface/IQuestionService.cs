using SchoolManagement.Model;
using SchoolManagement.ViewModel.Questions;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionService
    {

        List<QuestionsVM> getAllQuestions(int tenantId);
    }
}
