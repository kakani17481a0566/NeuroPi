using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestResult;

namespace SchoolManagement.Services.Interface
{
    public interface ITestResultService
    {
        List<ImageDb> GetResultImagesAsync(int studentId, int testId);

        string AddResult(TestResultRequestVM request);

        List<TestResultResponseVM> GetResult(int studentId, int testId, int relationId);
       
    }
}
