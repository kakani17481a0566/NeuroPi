using SchoolManagement.Model;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.ViewModel.Employee
{
    public class EmployeeRequestVM
    {
        public string EmpCode { get; set; }
        public int UserId { get; set; }
        public int DesignationId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }

        public static MEmployee ToModel(EmployeeRequestVM employeeRequestVM)
        {
            return new MEmployee
            {
                EmpCode = employeeRequestVM.EmpCode,
                UserId = employeeRequestVM.UserId,
                DesignationId = employeeRequestVM.DesignationId,
                BranchId = employeeRequestVM.BranchId,
                TenantId = employeeRequestVM.TenantId,
            };

        }
    }
}
