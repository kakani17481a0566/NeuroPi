namespace NeuroPi.UserManagment.ViewModel.Group
{
    public class GroupUpdateInputVM
    {
        public string Name { get; set; }   // Only Name is allowed to be updated, not TenantId
    }
}
