namespace NeuroPi.UserManagment.ViewModel.Group
{
    public class GroupUpdateWithTenantVM
    {
        // We no longer allow modifying TenantId here
        public string Name { get; set; }   // Only Name can be updated\

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
