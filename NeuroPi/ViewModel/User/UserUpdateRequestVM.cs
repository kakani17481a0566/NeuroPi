using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UserUpdateRequestVM
    {
        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }  // Optional during update
        public string? RoleName { get; set; }

        public string? MobileNumber { get; set; }
        public string? AlternateNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }

        public int? UpdatedBy { get; set; }
        public int? ParentId { get; set; } // Added ParentId
        public string? UserImageUrl { get; set; }
        public List<LinkedStudentVM> LinkedStudents { get; set; }
    }
}