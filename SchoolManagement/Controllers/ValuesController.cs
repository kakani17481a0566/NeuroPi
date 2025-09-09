using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly claculater _calculater;
        public ValuesController(claculater calculater)
        {
            _calculater = calculater;
        }
        [HttpPost]
        public int Add(int A,int B)
        {
           return _calculater.Add(A, B);
        }
        [HttpPatch]
        public int sub(int A, int B)
        {
            return _calculater.sub(A, B);
        }
        [HttpGet]
        public int mul (int A, int B)
        {
            return _calculater.mul(A,B);
        }
        [HttpDelete]
        public int div(int A,int B)
        {
            return _calculater.div(A,B);
        }
        [HttpOptions]
        public string fullName(string A, string B)
        {
            return _calculater.fullName(A, B);
        }
    }
}
