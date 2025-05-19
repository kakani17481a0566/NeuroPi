using System.Security.Cryptography.X509Certificates;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.UserDepartment
{
    public class UserDepartmentResponseVM
    {
        public int UserDeptId { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int TenantId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }



        public static UserDepartmentResponseVM ToViewModel(MUserDepartment userDepartment)
        {
            return new UserDepartmentResponseVM
            {
                UserDeptId = userDepartment.UserDeptId,
                UserId = userDepartment.UserId,
                DepartmentId = userDepartment.DepartmentId,
                TenantId = userDepartment.TenantId,
                CreatedBy = userDepartment.CreatedBy,
                CreatedOn = userDepartment.CreatedOn,
                UpdatedBy = userDepartment.UpdatedBy,
              
            };

            
        }

        public static List<UserDepartmentResponseVM> ToViewModelList(List<MUserDepartment> userDepartments)
        {
            return userDepartments.Select(userDepartment => ToViewModel(userDepartment)).ToList();
        }
    }
}
