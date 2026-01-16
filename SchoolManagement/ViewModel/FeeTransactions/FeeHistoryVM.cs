
namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class FeeHistoryVM
    {
        public string Month { get; set; } = string.Empty; // e.g., "Jan"
        public decimal Collected { get; set; }
        public decimal Generated { get; set; }
    }
}
