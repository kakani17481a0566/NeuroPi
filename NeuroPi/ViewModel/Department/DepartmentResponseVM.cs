using NeuroPi.Models;

namespace NeuroPi.ViewModel.Department
{
    public class DepartmentResponseVM
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int? HeadUserId { get; set; }

        public int OrganizationId { get; set; }

        public string OrganizationName { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }

        

        public static DepartmentResponseVM ToViewModel(MDepartment department)
        {
            return new DepartmentResponseVM
            {
                Id = department.DepartmentId,
                Name = department.Name,
                HeadUserId = department.HeadUserId,
                OrganizationId = department.OrganizationId,
                OrganizationName = department.Organization.Name,
                TenantName=department.Tenant.Name,
                TenantId = department.TenantId,
            };
        }

        public static List<DepartmentResponseVM> ToViewModelList(List<MDepartment> departments)
        {
            List<DepartmentResponseVM> result= new List<DepartmentResponseVM>();
            foreach(MDepartment department in departments)
            {
                result.Add(ToViewModel(department));
            }
            return result;
        }

    }
}
