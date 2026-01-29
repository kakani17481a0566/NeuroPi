using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransferController : ControllerBase
    {
        private readonly IInventoryTransferService _service;

        public InventoryTransferController(IInventoryTransferService service)
        {
            _service = service;
        }

        [HttpPost("CreateRequest")]
        public ResponseResult<InventoryTransferResponseVM> CreateRequest([FromBody] InventoryTransferRequestVM request)
        {
            return _service.CreateRequest(request);
        }

        [HttpPost("ApproveRequest")]
        public ResponseResult<bool> ApproveRequest([FromBody] InventoryTransferApprovalVM request)
        {
            return _service.ProcessApproval(request);
        }

        [HttpGet("GetMyRequests/{branchId}")]
        public ResponseResult<List<InventoryTransferResponseVM>> GetMyRequests(int branchId)
        {
            // Assuming TenantId 1 for now or fetching from User Context in real app
            int tenantId = 1; 
            return _service.GetRequestsByBranch(branchId, tenantId);
        }
    }
}
