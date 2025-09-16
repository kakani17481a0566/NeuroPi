using System.Net;

using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Test;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService=testService;
        }
        [HttpGet]
        public ResponseResult<List<TestResponseVM>> GetTest(int masterId)
        {
            var result=_testService.GetTestResults(masterId);
            return new ResponseResult<List<TestResponseVM>>(HttpStatusCode.OK, result,"retreived sucessfully");

        }
    }
}
