using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemBranch
{
    public class ItemBranchRequestVM
    {
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
        public int ItemCost { get; set; }
        public int ItemReOrderLevel { get; set; }
        public int? ItemLocationId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public MItemBranch ToModel()
        {
            return new MItemBranch
            {
                ItemId = this.ItemId,
                BranchId = this.BranchId,
                ItemQuantity = this.ItemQuantity,
                ItemPrice = this.ItemPrice,
                ItemCost = this.ItemCost,
                ItemReOrderLevel = this.ItemReOrderLevel,
                ItemLocationId = this.ItemLocationId,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }

    }
}
