using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageSessionController : ControllerBase
    {
        private readonly IPageSessionService _sessionService;

        public PageSessionController(IPageSessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost("start")]
        public ResponseResult<long> StartSession([FromBody] SessionStartRequest request)
        {
            var sessionId = _sessionService.StartSession(request.PageName, request.UserId, request.TenantId);
            return new ResponseResult<long>(HttpStatusCode.OK, sessionId, "Session started");
        }

        [HttpPost("end/{sessionId}")]
        public ResponseResult<bool> EndSession(long sessionId)
        {
            var result = _sessionService.EndSession(sessionId);
            if (result)
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Session ended");
            
            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Session not found");
        }
    }

    public class SessionStartRequest
    {
        public string PageName { get; set; }
        public int? UserId { get; set; }
        public int? TenantId { get; set; }
    }
}
