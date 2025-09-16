using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CountingTest;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountingTestController : ControllerBase
    {
        private readonly ICountingTestInterface _countingTestInterface;
        public CountingTestController(ICountingTestInterface countingTestInterface)
        {
            _countingTestInterface = countingTestInterface;
            
        }
        [HttpGet]
        public List<CountingResponseVM> GetResult(string shape,string label)
        {
            return _countingTestInterface.GetResult(shape, label);
        }
    }
}
