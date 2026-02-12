using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Carpidum;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarpidumController : ControllerBase
    {
        private readonly ICarpidumService _service;

        public CarpidumController(ICarpidumService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public ResponseResult<List<CarpidumVM>> GetAll([FromQuery] int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<CarpidumVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<CarpidumVM> GetById(int id, [FromQuery] int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<CarpidumVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<CarpidumVM>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpGet("student/{studentId}")]
        public ResponseResult<List<CarpidumVM>> GetByStudentId(int studentId, [FromQuery] int tenantId)
        {
            var result = _service.GetByStudentId(studentId, tenantId);
            return new ResponseResult<List<CarpidumVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpPost]
        public ResponseResult<CarpidumVM> Create([FromBody] CarpidumRequestVM request)
        {
            string message;
            var result = _service.Create(request, out message);

            if (result == null)
                return new ResponseResult<CarpidumVM>(HttpStatusCode.BadRequest, null, message);

            return new ResponseResult<CarpidumVM>(HttpStatusCode.Created, result, message);
        }

        [HttpPut("{id}")]
        public ResponseResult<CarpidumVM> Update(int id, [FromBody] CarpidumRequestVM request)
        {
            string message;
            var result = _service.Update(id, request, out message);

            if (result == null)
                return new ResponseResult<CarpidumVM>(HttpStatusCode.BadRequest, null, message);

            return new ResponseResult<CarpidumVM>(HttpStatusCode.OK, result, message);
        }

        [HttpDelete("{id}")]
        public ResponseResult<object> Delete(int id, [FromQuery] int tenantId)
        {
            var deleted = _service.Delete(id, tenantId);
            if (!deleted)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Record not found or failed to delete");

            return new ResponseResult<object>(HttpStatusCode.OK, null, "Deleted successfully");
        }
    }
}
