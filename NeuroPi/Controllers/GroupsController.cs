using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Group;
using NeuroPi.UserManagment.Response;

namespace NeuroPi.UserManagment.Controllers
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
        public ResponseResult<object> GetAll()
        {
            try
            {
                var groups = _groupService.GetAll();
                if (groups == null || groups.Count == 0)
                {
                    return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "No groups found");
                }

                return new ResponseResult<object>(System.Net.HttpStatusCode.OK, groups, "Groups fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Get a group by ID
        [HttpGet("{id}")]
        public ResponseResult<object> GetById(int id)
        {
            try
            {
                var group = _groupService.GetById(id);
                if (group == null)
                {
                    return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found");
                }

                return new ResponseResult<object>(System.Net.HttpStatusCode.OK, group, "Group fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Create a new group
        [HttpPost]
        public ResponseResult<object> Create([FromBody] GroupInputVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name) || input.TenantId <= 0)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "Invalid group data");
            }

            try
            {
                var group = _groupService.Create(input);
                return new ResponseResult<object>(System.Net.HttpStatusCode.Created, group, "Group created successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Update an existing group
        [HttpPut("{id}")]
        public ResponseResult<object> Update(int id, [FromBody] GroupUpdateInputVM input)
        {
            if (input == null || string.IsNullOrEmpty(input.Name))
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "Invalid group data");
            }

            try
            {
                var group = _groupService.Update(id, input);
                if (group == null)
                {
                    return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found");
                }

                return new ResponseResult<object>(System.Net.HttpStatusCode.OK, group, "Group updated successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        // Delete a group
        [HttpDelete("{id}")]
        public ResponseResult<object> Delete(int id)
        {
            try
            {
                var success = _groupService.Delete(id);
                if (!success)
                {
                    return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found");
                }

                return new ResponseResult<object>(System.Net.HttpStatusCode.NoContent, null, "Group deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }


        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<object> GetByTenantId(int tenantId)
        {
            try
            {
                var groups = _groupService.GetByTenantId(tenantId);
                if (groups == null || groups.Count == 0)
                {
                    return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "No groups found for the given tenant");
                }

                return new ResponseResult<object>(System.Net.HttpStatusCode.OK, groups, "Groups fetched successfully for the tenant");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

    }
}
