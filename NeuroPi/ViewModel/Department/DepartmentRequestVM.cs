using NeuroPi.Models;

namespace NeuroPi.ViewModel.Department
{
    public class DepartmentRequestVM
    {

        public string Name { get; set; }

        public int? HeadUserId { get; set; }

        public int TenantId { get; set; }

        public int OrganizationId { get; set; }

        public static MDepartment ToModel(DepartmentRequestVM departmentRequestVM)
        {
            return new MDepartment
            {
                Name = departmentRequestVM.Name,
                HeadUserId = departmentRequestVM.HeadUserId,
                TenantId = departmentRequestVM.TenantId,
                OrganizationId = departmentRequestVM.OrganizationId
            };
        }
    }
}
