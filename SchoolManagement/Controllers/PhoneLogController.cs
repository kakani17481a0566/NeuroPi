using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PhoneLog;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneLogController : ControllerBase
    {
        private readonly IPhoneLogService _phoneLogService;
        public PhoneLogController(IPhoneLogService phoneLogService)
        {
            _phoneLogService = phoneLogService;
        }
        [HttpGet]
         public ResponseResult<object> GetAll()
        {
            var result = _phoneLogService.GetAll();
            return new ResponseResult<object>(System.Net.HttpStatusCode.OK, result);
        }
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<object> GetByTenantId(int tenantId)
        {
            var result = _phoneLogService.GetByTenantId(tenantId);
            return new ResponseResult<object>(System.Net.HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public ResponseResult<object> GetBybranch_id(int id)
        {
            var result = _phoneLogService.GetBybranch_id(id);
            if (result == null)
                return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Phone log not found");
          
            return new ResponseResult<object>(System.Net.HttpStatusCode.OK, result);
        }
        [HttpPost]
        public ResponseResult<object> create([FromBody] PhoneLogRequestVM model)
        {
            var result = _phoneLogService.create(model);
            return new ResponseResult<object>(System.Net.HttpStatusCode.Created, result);
        }
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<object> update(int branch_id, int tenant_id, [FromBody] PhoneLogUpdateVM model)
        {
            var result = _phoneLogService.update(branch_id, tenant_id, model);
            if (result == null)
                return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Phone log not found");
            return new ResponseResult<object>(System.Net.HttpStatusCode.OK, result);
        }

        
    }
}
