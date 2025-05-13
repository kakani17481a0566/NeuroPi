using NeuroPi.UserManagment.ViewModel.Organization;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IOrganizationService
    {
        List<OrganizationVM> GetAll();
        OrganizationVM GetById(int id);
        OrganizationVM Create(OrganizationInputVM input);
        OrganizationVM Update(int id, OrganizationUpdateInputVM input);
        bool Delete(int id);

        List<OrganizationVM> GetByTenantId(int tenantId);

    }
}
