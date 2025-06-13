using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.Employee
{
    public class EmployeeResponseVM
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public int UserId { get; set; }
        public int DesignationId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }

        public static EmployeeResponseVM ToViewModel(MEmployee employee)
        {
            return new EmployeeResponseVM
            {
                Id = employee.Id,
                EmpCode = employee.EmpCode,
                UserId = employee.UserId,
                DesignationId = employee.DesignationId,
                BranchId = employee.BranchId,
                TenantId = employee.TenantId,
            };
        }
        public static List<EmployeeResponseVM> ToViewModelList(List<MEmployee> employees)
        {
            return employees.Select(ToViewModel).ToList();
        }
    }
}
