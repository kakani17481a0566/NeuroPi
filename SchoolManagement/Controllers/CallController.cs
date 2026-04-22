using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Call;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        private readonly ICallService callService;
        public CallController(ICallService _callService)
        {
            callService = _callService;
            
        }
        [HttpGet("{tenantId}")]
        public ResponseResult<List<CallResponseVM>> GetAllCallLogs(int tenantId)
        {
            var result=callService.GetAllLogs(tenantId);
            if(result == null)
            {
                return new ResponseResult<List<CallResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "Call logs Not Found");
            }
            return new ResponseResult<List<CallResponseVM>>(System.Net.HttpStatusCode.OK, result, "call logs fetched succcessfully");
        }
        [HttpGet("{empId}/tenant/{tenantId}")]
        public ResponseResult<List<CallResponseVM>> GetAllCallLogs(int empId,int tenantId)
        {
            var result = callService.GetAllEmployeeLogs(empId,tenantId);
            if (result == null)
            {
                return new ResponseResult<List<CallResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "Call logs Not Found");
            }
            return new ResponseResult<List<CallResponseVM>>(System.Net.HttpStatusCode.OK, result, "call logs fetched succcessfully");
        }
    }
}
