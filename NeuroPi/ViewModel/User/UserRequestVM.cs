using System.ComponentModel.DataAnnotations;

namespace NeuroPi.ViewModel.User
{
    // UserRequestVM.cs
    public class UserRequestVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? MobileNumber { get; set; }
        public string? AlternateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }

        [Required]
        public int? TenantId { get; set; }  // Make TenantId nullable


        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

  

    }
}
