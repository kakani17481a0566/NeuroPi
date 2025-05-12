namespace NeuroPi.ViewModel.UserRoles
{
    public class UserRoleUpdateVM
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int TenantId { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
