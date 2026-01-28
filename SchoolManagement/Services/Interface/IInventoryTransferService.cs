using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IInventoryTransferService
    {
        // Create request (Status: PENDING)
        ResponseResult<InventoryTransferResponseVM> CreateRequest(InventoryTransferRequestVM request);

        // Approve/Reject request (Atomic Stock Update)
        ResponseResult<bool> ProcessApproval(InventoryTransferApprovalVM approval);

        // View Requests
        ResponseResult<List<InventoryTransferResponseVM>> GetRequestsByBranch(int branchId, int tenantId);
    }
}
