using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemBranch;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemBranchController : ControllerBase
    {
        private readonly IItemBranchService _itemBranchService;
        public ItemBranchController(IItemBranchService itemBranchService)
        {
            _itemBranchService = itemBranchService;
        }

        [HttpGet("GetAllItemBranches")]

        public ResponseResult<List<ItemBranchResponseVM>> GetAllItemBranches()
        {
            var response = _itemBranchService.GetAllItemBranches();
            if (response == null)
            {
                return new ResponseResult<List<ItemBranchResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemBranchResponseVM>>(HttpStatusCode.OK, response, "ItemBranches fetched successfully");
        }

        [HttpGet("GetItemBranchesByTenant/{tenantId}")]
        public ResponseResult<List<ItemBranchResponseVM>> GetItemBranchesByTenant([FromRoute] int tenantId)
        {
            var response = _itemBranchService.GetItemBranchesByTenant(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<ItemBranchResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<ItemBranchResponseVM>>(HttpStatusCode.OK, response, "ItemBranches fetched successfully");
        }

        [HttpGet("GetItemBranchById/{id}")]
        public ResponseResult<ItemBranchResponseVM> GetItemBranchById([FromRoute] int id)
        {
            var response = _itemBranchService.GetItemBranchById(id);
            if (response != null)
            {
                return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.OK, response, "ItemBranch is fetched successfully");
            }
            return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.BadGateway, response, $" ItemBranch not found with id {id}");
        }
        [HttpGet("GetItemBranchByIdAndTenant/{id}/{tenantId}")]
        public ResponseResult<ItemBranchResponseVM> GetItemBranchByIdAndTenantId([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = _itemBranchService.GetItemBranchByIdAndTenant(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.OK, response, "ItemBranch is fetched successfully");
            }
            return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.BadGateway, response, $" ItemBranch not found with id {id} for the specified tenant");
        }

        [HttpPost("CreateItemBranch")]
        public ResponseResult<ItemBranchResponseVM> CreateItemBranch([FromBody] ItemBranchRequestVM itemBranchRequestVM)
        {
            var response = _itemBranchService.CreateItemBranch(itemBranchRequestVM);
            if (response != null)
            {
                return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.OK, response, "ItemBranch is created successfully");
            }
            return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.BadGateway, response, "Failed to create ItemBranch");
        }

        [HttpPut("UpdateItemBranch/{id}/{tenantId}")]
        public ResponseResult<ItemBranchResponseVM> UpdateItemBranch([FromRoute] int id, [FromRoute] int tenantId, [FromBody] ItemBranchUpdateVM itemBranchUpdateVM)
        {
            var response = _itemBranchService.UpdateItemBranch(id, tenantId, itemBranchUpdateVM);
            if (response != null)
            {
                return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.OK, response, "ItemBranch is updated successfully");
            }
            return new ResponseResult<ItemBranchResponseVM>(HttpStatusCode.BadGateway, response, $"Failed to update ItemBranch with id {id} for the specified tenant");
        }

        [HttpDelete("DeleteItemBranchByIdAndTenant/{id}/{tenantId}")]
        public ResponseResult<bool> DeleteItemBranchByIdAndTenant([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = _itemBranchService.DeleteItemBranchByIdAndTenant(id, tenantId);
            if (response)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, response, "ItemBranch is deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.BadGateway, response, $"Failed to delete ItemBranch with id {id} for the specified tenant");
        }

    }
}
