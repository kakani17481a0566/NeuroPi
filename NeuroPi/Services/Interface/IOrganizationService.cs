using NeuroPi.ViewModel.Group;
using NeuroPi.ViewModel.Organization;

namespace NeuroPi.Services.Interface
{
    public interface IOrganizationService
    {
        List<OrganizationVM> GetAll();
        OrganizationVM GetById(int id);
        OrganizationVM Create(OrganizationInputVM input);
        OrganizationVM Update(int id, OrganizationUpdateInputVM input);
        bool Delete(int id);
    }
}
