using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Inventory
{
    public class InventoryTransferRequestVM
    {
        public string TransferType { get; set; } // 'REFILL' or 'TRANSFER'
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int? FromBranchId { get; set; } // Required if TRANSFER
        public int ToBranchId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; } // Current User
    }

    public class InventoryTransferApprovalVM
    {
        public int Id { get; set; }
        public string Status { get; set; } // 'APPROVED' or 'REJECTED'
        public int ApprovedBy { get; set; } // Current User ID
    }

    public class InventoryTransferResponseVM
    {
        public int Id { get; set; }
        public string TransferType { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        
        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int? FromBranchId { get; set; }
        public string FromBranchName { get; set; }

        public int ToBranchId { get; set; }
        public string ToBranchName { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }
}
