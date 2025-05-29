namespace SchoolManagement.ViewModel.Item
{
    public class ItemRequestVM
    {
        public int ItemHeaderId { get; set; }
        public string BookCondition { get; set; }
        public string Status { get; set; }
        public int TenantId { get; set; }
        public DateTime? PurchasedOn { get; set; }
        public int CreatedBy { get; set; }
    }
     
    }
