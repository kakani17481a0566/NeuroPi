namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UsersProfileSummaryVM
    {
        // User Info
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? MobileNumber { get; set; }
        public string? UserImageUrl { get; set; }

        // Role Info (aggregated)
        public string Roles { get; set; } = string.Empty;   // comma-separated list

        // Course & Branch Info
        public int TotalCourses { get; set; }
        public string CoursesTaught { get; set; } = string.Empty;   // comma-separated
        public int TotalBranches { get; set; }
        public string Branches { get; set; } = string.Empty;        // comma-separated

        // Work Info (nullable from DB)
        public DateTime? JoiningDate { get; set; }                  // keep as native
        public TimeSpan? WorkingStartTime { get; set; }             // keep as native
        public TimeSpan? WorkingEndTime { get; set; }               // keep as native

        // Status
        public string UserStatus { get; set; } = "Active";

        // Audit Trail
        public DateTime UserCreatedOn { get; set; }
        public DateTime? UserLastUpdated { get; set; }              // nullable
        public DateTime RoleAssignedOn { get; set; }
    }
}
