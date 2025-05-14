using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.AuditLog;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {

        private readonly IAuditLogService _auditLogService;
        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
            
        }
        [HttpPost]
        public ResponseResult<AuditLogResponseVM> AddAuditLog([FromBody] AuditRequestVM auditRequestVM)
        {
            var result=_auditLogService.AddAuditLog(auditRequestVM);
            if (result != null)
            {
                return new ResponseResult<AuditLogResponseVM>(HttpStatusCode.Created, result, "Successfully created audit");
            }
            return new ResponseResult<AuditLogResponseVM>(HttpStatusCode.BadGateway, null, "Auditlog Not Created ");
        }

    }
}
