namespace SchoolManagement.ViewModel.Transaction
{
    public class UpdateTrxAmountRequestVM
    {
        public string RefTrnsId { get; set; }
        public int TenantId { get; set; }
        public double NewAmount { get; set; }
        public int ModifiedBy { get; set; }
    }
}
