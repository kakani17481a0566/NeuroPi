namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UserLogInSucessVM
    {

        public string UserName { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }
        public string token { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public  int departmentId { get; set; }

        public byte[]? UserImageUrl { get; set; }
        public UserResponseVM UserProfile { get; set; }




    }
}