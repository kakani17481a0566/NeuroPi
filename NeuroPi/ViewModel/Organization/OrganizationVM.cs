namespace NeuroPi.UserManagment.ViewModel.Organization
{
    public class OrganizationVM
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int TenantId { get; set; }
    }
}
