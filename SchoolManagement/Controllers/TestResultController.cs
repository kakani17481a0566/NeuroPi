using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Audio;
using SchoolManagement.ViewModel.TestResult;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    { 
        private readonly ITestResultService _testResultService;
        public TestResultController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
            
        }
        [HttpGet("/{testId}/{studentId}")]
        public List<ImageDb> GetImages(int testId,int studentId)
        {
            var res=_testResultService.GetResultImagesAsync( studentId,testId);
            return res;
        }
        [HttpPost]
        public string AddResult(TestResultRequestVM request)
        {
            var res=_testResultService.AddResult(request);
            return res;
        }
        [HttpGet("/result")]
        public List<TestResultResponseVM> GetTestResults([FromQuery] int testId, [FromQuery] int studentId, [FromQuery] int relationId)
        {
            var result=_testResultService.GetResult(studentId,testId,relationId);
            if (result == null) return [];
            return result;
        }

    }
}
