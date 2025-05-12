using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.GroupUser;

namespace NeuroPi.Controllers
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

        [HttpPut("update-by-id/{id}")]
        public ResponseResult<GroupUserUpdateVM> UpdateById(int id, [FromBody] GroupUserUpdateVM input)
        {
            var updatedGroupUser = _groupUserService.updateGroupUserById(id, input);
            if (updatedGroupUser != null)
            {
                return new ResponseResult<GroupUserUpdateVM>(HttpStatusCode.OK, updatedGroupUser, "Group User updated successfully");
            }
            return new ResponseResult<GroupUserUpdateVM>(HttpStatusCode.NotFound, null, "Group User not found");
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

        [HttpGet("get-by-id/{GroupId}")]
        public ResponseResult<GroupUserVM> GetGroupUserById(int GroupId)
        {
            var groupUser = _groupUserService.getGroupUserById(GroupId);
            if (groupUser != null)
            {
                return new ResponseResult<GroupUserVM>(HttpStatusCode.OK, groupUser, "Group User retrieved successfully");
            }
            return new ResponseResult<GroupUserVM>(HttpStatusCode.NotFound, null, "Group User not found");
        }

        [HttpDelete("{GroupId}")]
        public ResponseResult<bool> DeleteGroupUserById(int GroupId)
        {
            var success = _groupUserService.deleteGroupUserById(GroupId);
            if (success)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Group User deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Group User not found");
        }
    }
}
