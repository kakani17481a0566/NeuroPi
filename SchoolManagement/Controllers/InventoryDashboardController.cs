using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryDashboardController : ControllerBase
    {
        private readonly IInventoryDashboardService _service;

        public InventoryDashboardController(IInventoryDashboardService service)
        {
            _service = service;
        }

        [HttpGet("{branchId}")]
        public ResponseResult<InventoryDashboardVM> GetDashboard(int branchId)
        {
            // Default TenantId for now
            int tenantId = 1;
            return _service.GetBranchDashboard(branchId, tenantId);
        }
    }
}
