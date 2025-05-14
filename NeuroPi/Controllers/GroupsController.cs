using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Group;

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

    [HttpPut("{id}/tenant/{tenantId}")]
    public ResponseResult<object> Update(int id, int tenantId, [FromBody] GroupUpdateWithTenantVM input)
    {
        if (input == null || string.IsNullOrEmpty(input.Name))
        {
            return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "Invalid group data");
        }

        try
        {
            var group = _groupService.GetById(id);
            if (group == null)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found");
            }

            // Ensure the TenantId from the URL matches the TenantId of the group in the database
            if (group.TenantId != tenantId)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "TenantId does not match");
            }

            // Validate that TenantId is not being modified during the update
            // Here we ensure the input does not have TenantId, and the existing TenantId is maintained
            if (group.TenantId != tenantId)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "TenantId cannot be modified");
            }

            // Proceed with the update (we update only the Name, not the TenantId)
            var updatedGroup = _groupService.Update(id, new GroupUpdateInputVM { Name = input.Name });

            return updatedGroup == null
                ? new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found")
                : new ResponseResult<object>(System.Net.HttpStatusCode.OK, updatedGroup, "Group updated successfully");
        }
        catch (Exception ex)
        {
            return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
        }
    }




    // Delete a group
    [HttpDelete("{id}/tenant/{tenantId}")]
    public ResponseResult<object> Delete(int id, int tenantId)  // Removed [FromQuery] here
    {
        try
        {
            // Fetch the group by ID
            var group = _groupService.GetById(id);
            if (group == null)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found");
            }

            // Validate that the TenantId in the request matches the TenantId of the group
            if (group.TenantId != tenantId)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.BadRequest, null, "TenantId does not match");
            }

            // Soft delete the group
            var success = _groupService.Delete(id);
            if (!success)
            {
                return new ResponseResult<object>(System.Net.HttpStatusCode.NotFound, null, "Group not found or already deleted");
            }

            return new ResponseResult<object>(System.Net.HttpStatusCode.NoContent, null, "Group deleted successfully");
        }
        catch (Exception ex)
        {
            return new ResponseResult<object>(System.Net.HttpStatusCode.InternalServerError, null, ex.Message);
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
