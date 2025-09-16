using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Test;

namespace SchoolManagement.Services.Implementation
{
    public class TestServiceImpl : ITestService
    {
        private readonly SchoolManagementDb _context;
        public TestServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public List<TestResponseVM> GetTestResults()
        {
           var result=_context.test.Where(t=> !t.isDeleted && t.id!=1).ToList();
            if (result != null)
            {
                return TestResponseVM.FromModel(result);
            }
            return new List<TestResponseVM>();
        }
    }
}
