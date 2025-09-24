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

        // Role Info
        public string RoleName { get; set; } = string.Empty;

        // Course & Branch Info
        public int TotalCourses { get; set; }
        public string CoursesTaught { get; set; } = string.Empty;   // comma-separated
        public int TotalBranches { get; set; }
        public string Branches { get; set; } = string.Empty;        // comma-separated

        // Work Info
        public string JoiningDate { get; set; } = "N/A";
        public string WorkingStartTime { get; set; } = "N/A";
        public string WorkingEndTime { get; set; } = "N/A";

        // Status
        public string UserStatus { get; set; } = "Active";

        // Audit Trail
        public string UserCreatedOn { get; set; } = string.Empty;
        public string UserLastUpdated { get; set; } = string.Empty;
        public string RoleAssignedOn { get; set; } = string.Empty;
    }
}
