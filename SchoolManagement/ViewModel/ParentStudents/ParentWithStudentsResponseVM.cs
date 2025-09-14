namespace SchoolManagement.ViewModel.ParentStudents
{
    public class ParentWithStudentsResponseVM
    {
        // Optional headers for front-end mapping
        public Dictionary<string, string> Headers { get; set; } = new()
        {
            { "parentId", "Parent ID" },
            { "userId", "User ID" },
            { "parentName", "Parent Name" },
            { "email", "Email Address" },
            { "mobileNumber", "Mobile Number" },
            { "tenantId", "Tenant ID" },
            { "roleName", "Role" },            // ✅ new
            { "students", "Linked Children" }
        };

        public ParentVM Parent { get; set; }
        public List<StudentVM> Students { get; set; } = new();
    }

    public class ParentVM
    {
        public int ParentId { get; set; }
        public int UserId { get; set; }
        public string ParentName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int TenantId { get; set; }
        public int? RoleTypeId { get; set; }
        public string? RoleTypeName { get; set; }
    }

    public class StudentVM
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string BranchName { get; set; }
        public string StudentImageUrl { get; set; }

        // ✅ Additional details for richer profile
        public DateOnly? Dob { get; set; }
        public int? Age { get; set; }
        public string? BloodGroup { get; set; }
    }
}
