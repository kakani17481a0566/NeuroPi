using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Models;
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
        public ResponseResult<GroupUserVM> create([FromBody] GroupUserInputVM groupUserInput)
        {
            var createGroupUser = _groupUserService.createGroupUser(groupUserInput);
            return ResponseResult<GroupUserVM>.SuccessResponse(HttpStatusCode.OK, createGroupUser, "Group User Created Successfully");
        }

        [HttpPut("update-by-id/{id}")]

        public ResponseResult<GroupUserUpdateVM> updateByid(int id, [FromBody] GroupUserUpdateVM input)
        {
            var groupUser = _groupUserService.updateGroupUserById(id, input);
            if (groupUser == null)
            {
                return ResponseResult<GroupUserUpdateVM>.FailResponse(HttpStatusCode.NotFound, "Group User Not Found");

            }
            return ResponseResult<GroupUserUpdateVM>.SuccessResponse(HttpStatusCode.OK, groupUser, "Group User Updated Succefully");
        }

        [HttpGet]
        public ResponseResult<List<GroupUserVM>> getAllGroupUsers()
        {
            var groupUsers = _groupUserService.getAllGroupUsers();
            if (groupUsers == null || groupUsers.Count == 0)
            {
                return ResponseResult<List<GroupUserVM>>.FailResponse(HttpStatusCode.NotFound, "Group Users Not Found");
            }
            return ResponseResult<List<GroupUserVM>>.SuccessResponse(HttpStatusCode.OK, groupUsers, "Group Users retrived Successfully");
        }

        [HttpGet("get-by-id/{GroupId}")]
        public ResponseResult<GroupUserVM> getGroupUserById(int GroupId)
        {
            var groupUser = _groupUserService.getGroupUserById(GroupId);
            if (groupUser == null)
            {
                return ResponseResult<GroupUserVM>.FailResponse(HttpStatusCode.NotFound, "Group User Not Found");
            }
            return ResponseResult<GroupUserVM>.SuccessResponse(HttpStatusCode.OK, groupUser, "Group User retrived Successfully");
        }

        [HttpDelete("{GroupId}")]
        public ResponseResult<bool> deleteGroupUserById(int GroupId)
        {
            var result = _groupUserService.deleteGroupUserById(GroupId);
            if (!result)
            {
                return ResponseResult<bool>.FailResponse(HttpStatusCode.NotFound,"Group User Not Found");
            }
            return ResponseResult<bool>.SuccessResponse(HttpStatusCode.OK,true, "Group User Deleted Successfully");
        }


    }
}
