using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Department
{
    public class DepartmentResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public int? HeadUserId { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public static DepartmentResponseVM ToViewModel(MDepartment department)
        {
            return new DepartmentResponseVM
            {
                Id = department.DepartmentId,
                Name = department.Name,
                TenantId = department.TenantId,
                TenantName = department.Tenant?.Name ?? "Unknown Tenant",
                HeadUserId = department.HeadUserId,
                OrganizationId = department.OrganizationId,
                OrganizationName = department.Organization?.Name ?? "Unknown Organization"
            };
        }

        public static List<DepartmentResponseVM> ToViewModelList(List<MDepartment> departments)
        {
            return departments.Select(d => ToViewModel(d)).ToList();
        }
    }
}
