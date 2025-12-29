namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UserWithRoleVM
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
        public string? UserImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public int TenantId { get; set; }
        
        // Role information
        public string? RoleName { get; set; }
        public int? RoleId { get; set; }
        
        // Status
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
