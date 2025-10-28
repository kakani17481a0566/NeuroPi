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
            { "roleName", "Role" },
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

        // 🔹 Additional fields from MUser
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public byte[]? UserImageUrl { get; set; }
        public string? AlternateNumber { get; set; }
        public string? Address { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        // Extended profile
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? SpouseName { get; set; }
        public string? MaritalStatus { get; set; }
        public DateOnly? WeddingAnniversaryDate { get; set; }
        public DateOnly? JoiningDate { get; set; }
        public TimeOnly? WorkingStartTime { get; set; }
        public TimeOnly? WorkingEndTime { get; set; }
    }

    public class StudentVM
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string CourseName { get; set; }
        public int? CourseId { get; set; }
        public string BranchName { get; set; }
        public byte[]? StudentImageUrl { get; set; }

        // 🔹 Fields actually available in MStudent
        public DateOnly? Dob { get; set; }               // maps to DateOfBirth
        public int? Age { get; set; }                    // calculated from DateOfBirth
        public string? Gender { get; set; }
        public string? BloodGroup { get; set; }
        public string? AdmissionNumber { get; set; }     // maps to RegNumber
        public string? AdmissionGrade { get; set; }
        public DateOnly? DateOfJoining { get; set; }
    }
}
