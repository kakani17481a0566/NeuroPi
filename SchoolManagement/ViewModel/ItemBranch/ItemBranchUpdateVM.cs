namespace SchoolManagement.ViewModel.ItemBranch
{
    public class ItemBranchUpdateVM
    {
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemPrice { get; set; }
        public int ItemCost { get; set; }
        public int ItemReOrderLevel { get; set; }
        public int ItemLocationId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
