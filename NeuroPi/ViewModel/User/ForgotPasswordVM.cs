using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.User
{
    public class ForgotPasswordRequestVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;
    }

    public class ValidateOtpRequestVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(10)]
        public string Otp { get; set; } = default!;
    }

    public class ResetPasswordOtpRequestVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(10)]
        public string Otp { get; set; } = default!;

        [Required]
        public string NewPassword { get; set; } = default!;
    }
}
