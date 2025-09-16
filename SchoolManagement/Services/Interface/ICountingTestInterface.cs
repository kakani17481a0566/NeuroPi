using SchoolManagement.ViewModel.CountingTest;

namespace SchoolManagement.Services.Interface
{
    public interface ICountingTestInterface
    {
        List<CountingResponseVM> GetResult(string label, string shape);
    }
}
