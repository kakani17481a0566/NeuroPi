using SchoolManagement.ViewModel.Test;

namespace SchoolManagement.Services.Interface
{
    public interface ITestService
    {
        List<TestResponseVM> GetTestResults(int masterId);
    }
}
