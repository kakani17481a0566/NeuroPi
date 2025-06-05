using SchoolManagement.ViewModel.Parent;

namespace SchoolManagement.Services.Interface
{
    public interface IParentService
    {
        List<ParentResponseVM> GetAllParents();
        ParentResponseVM GetParentById(int id);
        ParentResponseVM GetParentByIdAndTenantId(int id, int tenantId);

        List<ParentResponseVM> GetParentByTenantId(int tenantId);
        ParentResponseVM AddParent(ParentRequestVM parent);

        ParentResponseVM UpdateParent(int id, int tenantId, ParentUpdateVM parent);

        bool DeleteParent(int id, int tenantId);
    }
}
