using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Parent;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _parentService;
        public ParentController(IParentService parentService)
        {
            _parentService = parentService;
        }

        [HttpGet]
        public ResponseResult<List<ParentResponseVM>> GetAllParents()
        {
            var parents = _parentService.GetAllParents();
            return new ResponseResult<List<ParentResponseVM>>(HttpStatusCode.OK, parents, "All parents retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<ParentResponseVM> GetParentById(int id)
        {
            var parent = _parentService.GetParentById(id);
            return parent == null
                ? new ResponseResult<ParentResponseVM>(HttpStatusCode.NotFound, null, "Parent not found")
                : new ResponseResult<ParentResponseVM>(HttpStatusCode.OK, parent, "Parent retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<ParentResponseVM>> GetParentByTenantId(int tenantId)
        {
            var parents = _parentService.GetParentByTenantId(tenantId);
            return parents == null
                ? new ResponseResult<List<ParentResponseVM>>(HttpStatusCode.NotFound, null, "No parents found for the specified tenant")
                : new ResponseResult<List<ParentResponseVM>>(HttpStatusCode.OK, parents, "Parents retrieved successfully");
        }
        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<ParentResponseVM> GetParentByIdAndTenantId(int id, int tenantId)
        {
            var parent = _parentService.GetParentByIdAndTenantId(id, tenantId);
            return parent == null
                ? new ResponseResult<ParentResponseVM>(HttpStatusCode.NotFound, null, "Parent not found for the specified ID and Tenant ID")
                : new ResponseResult<ParentResponseVM>(HttpStatusCode.OK, parent, "Parent retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<ParentResponseVM> AddParent(ParentRequestVM parent)
        {
            if (parent == null)
            {
                return new ResponseResult<ParentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid parent data");
            }
            var addedParent = _parentService.AddParent(parent);
            return new ResponseResult<ParentResponseVM>(HttpStatusCode.Created, addedParent, "Parent added successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ParentResponseVM> UpdateParent(int id, int tenantId, ParentUpdateVM parent)
        {
            if (parent == null)
            {
                return new ResponseResult<ParentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid parent data");
            }
            var updatedParent = _parentService.UpdateParent(id, tenantId, parent);
            return updatedParent == null
                ? new ResponseResult<ParentResponseVM>(HttpStatusCode.NotFound, null, "Parent not found for the specified ID and Tenant ID")
                : new ResponseResult<ParentResponseVM>(HttpStatusCode.OK, updatedParent, "Parent updated successfully");
        }
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteParent(int id, int tenantId)
        {
            var isDeleted = _parentService.DeleteParent(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Parent deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Parent not found for the specified ID and Tenant ID");

        }
    }
}
