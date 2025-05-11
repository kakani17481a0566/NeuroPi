using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Group;
using System;

namespace NeuroPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        // Injecting IGroupService via constructor
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // Get all groups
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var groups = _groupService.GetAll();
                if (groups == null || groups.Count == 0)
                {
                    return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.NotFound, "No groups found");
                }

                return ResponseResult<object>.SuccessResponse(System.Net.HttpStatusCode.OK, groups);
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Get a group by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var group = _groupService.GetById(id);
                if (group == null)
                {
                    return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.NotFound, "Group not found");
                }

                return ResponseResult<object>.SuccessResponse(System.Net.HttpStatusCode.OK, group);
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Create a new group
        [HttpPost]
        public IActionResult Create([FromBody] GroupInputVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name) || input.TenantId <= 0)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.BadRequest, "Invalid group data");
            }

            try
            {
                var group = _groupService.Create(input);
                return ResponseResult<object>.SuccessResponse(System.Net.HttpStatusCode.Created, group, "Group created successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Update an existing group
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GroupUpdateInputVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name))
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.BadRequest, "Invalid group data");
            }

            try
            {
                var group = _groupService.Update(id, input);
                if (group == null)
                {
                    return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.NotFound, "Group not found");
                }

                return ResponseResult<object>.SuccessResponse(System.Net.HttpStatusCode.OK, group, "Group updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Delete a group
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _groupService.Delete(id);
                if (!success)
                {
                    return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.NotFound, "Group not found");
                }

                return ResponseResult<object>.SuccessResponse(System.Net.HttpStatusCode.NoContent, null, "Group deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
