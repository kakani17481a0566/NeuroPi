using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ParentStudents;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentStudentsController : ControllerBase
    {
        private readonly IParentStudentsService _service;

        public ParentStudentsController(IParentStudentsService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ParentStudentRequestVM request)
        {
            var result = _service.Create(request);
            return new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result);
        }

        [HttpGet("tenant/{tenantId}")]
        public IActionResult GetAllByTenantId(int tenantId)
        {
            var result = _service.GetAllByTenantId(tenantId);
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return result == null
                ? new ResponseResult<string>(HttpStatusCode.NotFound, null, "Not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public IActionResult GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<string>(HttpStatusCode.NotFound, null, "Not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public IActionResult Update(int id, int tenantId, [FromBody] ParentStudentUpdateVM request)
        {
            var result = _service.UpdateByIdAndTenantId(id, tenantId, request);
            return result == null
                ? new ResponseResult<string>(HttpStatusCode.NotFound, null, "Update failed")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public IActionResult Delete(int id, int tenantId)
        {
            var result = _service.DeleteByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<string>(HttpStatusCode.NotFound, null, "Delete failed")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Deleted successfully");
        }
    }
}
