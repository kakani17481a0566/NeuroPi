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
        public ResponseResult<ParentStudentResponseVM> Create([FromBody] ParentStudentRequestVM request)
        {
            var result = _service.Create(request);
            return new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Created successfully");
        }

        [HttpGet]
        public ResponseResult<List<ParentStudentResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<ParentStudentResponseVM>> GetAllByTenantId(int tenantId)
        {
            var result = _service.GetAllByTenantId(tenantId);
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<ParentStudentResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> Update(int id, int tenantId, [FromBody] ParentStudentUpdateVM request)
        {
            var result = _service.UpdateByIdAndTenantId(id, tenantId, request);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Update failed")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> Delete(int id, int tenantId)
        {
            var result = _service.DeleteByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Delete failed")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Deleted successfully");
        }

        [HttpGet("full-details/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<ParentWithStudentsResponseVM> GetFullDetailsByUser(int userId, int tenantId)
        {
            var result = _service.GetFullParentDetailsByUserId(userId, tenantId);

            return result == null
                ? new ResponseResult<ParentWithStudentsResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Parent not found for given user and tenant")
                : new ResponseResult<ParentWithStudentsResponseVM>(
                    HttpStatusCode.OK,
                    result,
                    "Fetched parent and linked students successfully");
        }


    }
}
