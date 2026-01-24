using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemBranch
{
    public class ItemBranchResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
        public int ItemCost { get; set; }
        public int ItemReOrderLevel { get; set; }
        public int ItemLocationId { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? CategoryName { get; set; }

        public static ItemBranchResponseVM ToViewModel(MItemBranch itemBranch)
        {
            return new ItemBranchResponseVM
            {
                Id = itemBranch.Id,
                ItemId = itemBranch.ItemId,
                BranchId = itemBranch.BranchId,
                ItemQuantity = itemBranch.ItemQuantity,
                ItemPrice = itemBranch.ItemPrice,
                ItemCost = itemBranch.ItemCost,
                ItemReOrderLevel = itemBranch.ItemReOrderLevel,
                ItemLocationId = itemBranch.ItemLocationId,
                TenantId = itemBranch.TenantId,
                CreatedBy = itemBranch.CreatedBy,
                CreatedOn = itemBranch.CreatedOn,
                UpdatedBy = itemBranch.UpdatedBy,
                UpdatedOn = itemBranch.UpdatedOn,
            };
        }

        public List<ItemBranchResponseVM> ToViewModelList(List<MItemBranch> itemBranchList)
        {
            List<ItemBranchResponseVM> result = new List<ItemBranchResponseVM>();
            foreach (var itemBranch in itemBranchList)
            {
                result.Add(ToViewModel(itemBranch));
            }
            return result;
        }
    }
}
