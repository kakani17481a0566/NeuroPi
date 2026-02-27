using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.User
{
    public class ForgotPasswordRequestVM
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Username { get; set; }
    }

    public class ValidateOtpRequestVM
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Username { get; set; }

        [Required]
        [StringLength(10)]
        public string Otp { get; set; } = default!;
    }

    public class ResetPasswordOtpRequestVM
    {
        [EmailAddress]
        public string? Email { get; set; }

        public string? Username { get; set; }

        [Required]
        [StringLength(10)]
        public string Otp { get; set; } = default!;

        [Required]
        public string NewPassword { get; set; } = default!;
    }
}
