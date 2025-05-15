using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Group;
using System;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // Get all groups
        [HttpGet]
        public ResponseResult<object> GetAll()
        {
            try
            {
                var groups = _groupService.GetAll();
                if (groups == null || groups.Count == 0)
                {
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "No groups found");
                }
                return new ResponseResult<object>(HttpStatusCode.OK, groups, "Groups fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Get a group by groupId
        [HttpGet("{groupId}")]
        public ResponseResult<object> GetByGroupId(int groupId)
        {
            try
            {
                var group = _groupService.GetByGroupId(groupId);
                if (group == null)
                {
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Group not found");
                }
                return new ResponseResult<object>(HttpStatusCode.OK, group, "Group fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Get groups by TenantId
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<object> GetByTenantId(int tenantId)
        {
            try
            {
                var groups = _groupService.GetByTenantId(tenantId);
                if (groups == null || groups.Count == 0)
                {
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "No groups found for the given tenant");
                }
                return new ResponseResult<object>(HttpStatusCode.OK, groups, "Groups fetched successfully for the tenant");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Get group by TenantId and GroupId
        [HttpGet("tenant/{tenantId}/group/{groupId}")]
        public ResponseResult<object> GetByTenantAndGroupId(int tenantId, int groupId)
        {
            try
            {
                var group = _groupService.GetByTenantAndGroupId(tenantId, groupId);
                if (group == null)
                {
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Group not found for the given tenant");
                }
                return new ResponseResult<object>(HttpStatusCode.OK, group, "Group fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Create a new group
        [HttpPost]
        public ResponseResult<object> Create([FromBody] GroupInputVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name) || input.TenantId <= 0)
            {
                return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Invalid group data");
            }

            try
            {
                var group = _groupService.Create(input);
                if (group == null)
                {
                    return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Invalid TenantId");
                }
                return new ResponseResult<object>(HttpStatusCode.Created, group, "Group created successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Update an existing group
        [HttpPut("{groupId}/tenant/{tenantId}")]
        public ResponseResult<object> Update(int groupId, int tenantId, [FromBody] GroupUpdateWithTenantVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name))
            {
                return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Invalid group data");
            }

            try
            {
                var group = _groupService.GetByGroupId(groupId);
                if (group == null)
                {
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Group not found");
                }

                // Ensure the TenantId from the URL matches the TenantId of the group in the database
                if (group.TenantId != tenantId)
                {
                    return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "TenantId does not match");
                }

                // Proceed with the update
                var updatedGroup = _groupService.Update(groupId, new GroupUpdateInputVM { Name = input.Name });

                return updatedGroup == null
                    ? new ResponseResult<object>(HttpStatusCode.NotFound, null, "Group not found")
                    : new ResponseResult<object>(HttpStatusCode.OK, updatedGroup, "Group updated successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Delete a group
        [HttpDelete("{groupId}/tenant/{tenantId}")]
        public ResponseResult<bool> Delete(int groupId, int tenantId)
        {
            try
            {
                var success = _groupService.DeleteById(groupId, tenantId);
                return new ResponseResult<bool>(
                    success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                    success,
                    success ? "Group deleted successfully" : "Delete failed: Group not found or already deleted"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, ex.Message);
            }
        }
    }
}
