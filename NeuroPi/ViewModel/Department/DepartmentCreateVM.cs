using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Department
{
    public class DepartmentCreateVM
    {
        public string Name { get; set; }
        public int TenantId { get; set; }
        public int HeadUserId { get; set; }
        public int OrganizationId { get; set; }
        public int CreatedBy { get; set; }

        public static MDepartment ToModel(DepartmentCreateVM vm)
        {
            return new MDepartment
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                HeadUserId = vm.HeadUserId,
                OrganizationId = vm.OrganizationId,
                CreatedBy = vm.CreatedBy
            };
        }
    }
}
