namespace NeuroPi.UserManagment.ViewModel.Tenent
{
    public class TenantVM
    {
        public int TenantId { get; set; }
        public string Name { get; set; }

        // Optional: include audit fields if needed in the response
        public DateTime CreatedOn { get; set; }

        public int UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
