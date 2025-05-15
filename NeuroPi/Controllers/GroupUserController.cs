using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.GroupUser;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IGroupUserService _groupUserService;

        public GroupUserController(IGroupUserService groupUserService)
        {
            _groupUserService = groupUserService;
        }

        [HttpPost]
        public ResponseResult<GroupUserVM> Create([FromBody] GroupUserInputVM groupUserInput)
        {
            var createdGroupUser = _groupUserService.createGroupUser(groupUserInput);
            if (createdGroupUser != null)
            {
                return new ResponseResult<GroupUserVM>(HttpStatusCode.OK, createdGroupUser, "Group User created successfully");
            }
            return new ResponseResult<GroupUserVM>(HttpStatusCode.BadRequest, null, "Failed to create Group User");
        }

        [HttpGet("{groupUserId}")]
        public ResponseResult<GroupUserVM> GetGroupUserByGroupUserId(int groupUserId)
        {
            var groupUser = _groupUserService.getGroupUserByGroupUserId(groupUserId);
            if (groupUser != null)
            {
                return new ResponseResult<GroupUserVM>(HttpStatusCode.OK, groupUser, "Group User retrieved successfully");
            }
            return new ResponseResult<GroupUserVM>(HttpStatusCode.NotFound, null, "Group User not found");
        }

        [HttpGet("{groupUserId}/tenant/{tenantId}")]
        public ResponseResult<GroupUserVM> GetGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId)
        {
            var groupUser = _groupUserService.getGroupUserByGroupUserIdAndTenantId(groupUserId, tenantId);
            if (groupUser != null)
            {
                return new ResponseResult<GroupUserVM>(HttpStatusCode.OK, groupUser, "Group User retrieved successfully");
            }
            return new ResponseResult<GroupUserVM>(HttpStatusCode.NotFound, null, "Group User not found");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<GroupUserVM>> GetGroupUsersByTenantId(int tenantId)
        {
            var groupUsers = _groupUserService.getGroupUsersByTenantId(tenantId);
            if (groupUsers != null && groupUsers.Count > 0)
            {
                return new ResponseResult<List<GroupUserVM>>(HttpStatusCode.OK, groupUsers, "Group Users retrieved successfully for tenant");
            }

            return new ResponseResult<List<GroupUserVM>>(HttpStatusCode.NotFound, null, "No Group Users found for tenant");
        }

        [HttpGet]
        public ResponseResult<List<GroupUserVM>> GetAllGroupUsers()
        {
            var groupUsers = _groupUserService.getAllGroupUsers();
            if (groupUsers != null && groupUsers.Count > 0)
            {
                return new ResponseResult<List<GroupUserVM>>(HttpStatusCode.OK, groupUsers, "Group Users retrieved successfully");
            }
            return new ResponseResult<List<GroupUserVM>>(HttpStatusCode.NotFound, null, "No Group Users found");
        }

        [HttpPut("{groupUserId}/tenant/{tenantId}")]
        public ResponseResult<GroupUserUpdateVM> UpdateByGroupUserIdAndTenantId(int groupUserId, int tenantId, [FromBody] GroupUserUpdateVM input)
        {
            if (input == null)
            {
                return new ResponseResult<GroupUserUpdateVM>(HttpStatusCode.BadRequest, null, "Invalid input data");
            }

            var updatedGroupUser = _groupUserService.updateGroupUserByIdAndTenantId(groupUserId, tenantId, input);
            if (updatedGroupUser != null)
            {
                return new ResponseResult<GroupUserUpdateVM>(HttpStatusCode.OK, updatedGroupUser, "Group User updated successfully");
            }

            return new ResponseResult<GroupUserUpdateVM>(HttpStatusCode.NotFound, null, "Group User not found or update failed");
        }

        [HttpDelete("{groupUserId}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId)
        {
            var success = _groupUserService.deleteGroupUserByGroupUserIdAndTenantId(groupUserId, tenantId);
            if (success)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Group User deleted successfully");
            }

            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Group User not found");
        }

    }
}
