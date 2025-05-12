namespace NeuroPi.ViewModel.UserRoles
{
    public class UserRoleCreateVM
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int TenantId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
