namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UserContactUpdateVM
    {
        public string Email { get; set; } = default!;
        public string? MobileNumber { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
