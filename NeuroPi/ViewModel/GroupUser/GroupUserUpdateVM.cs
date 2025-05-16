    using System.ComponentModel.DataAnnotations;

    namespace NeuroPi.UserManagment.ViewModel.GroupUser
    {
        public class GroupUserUpdateVM
        {
            public int GroupId { get; set; }

            public int UserId { get; set; }




            [Required]
            public int? UpdatedBy { get; set; }


        }
    }
