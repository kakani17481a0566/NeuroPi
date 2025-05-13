using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.UserDepartment
{
    public class UserDepartmentRequestVM
    {
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 

        //public int UpdatedBy { get; set; }

        public static MUserDepartment ToModel(UserDepartmentRequestVM userDepartment)
        {
            return new MUserDepartment
            {
                UserId = userDepartment.UserId,
                DepartmentId = userDepartment.DepartmentId,
                TenantId = userDepartment.TenantId,
                CreatedBy = userDepartment.CreatedBy,
                //UpdatedBy = userDepartment.UpdatedBy
            };
        }
    }
        
}
