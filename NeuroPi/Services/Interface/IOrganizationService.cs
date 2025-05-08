using NeuroPi.ViewModel.Group;
using NeuroPi.ViewModel.Organization;

namespace NeuroPi.Services.Interface
{
    public interface IOrganizationService
    {
        List<OrganizationViewModel> GetAll();
        OrganizationViewModel GetById(int id);
        OrganizationViewModel Create(OrganizationInputModel input);
        OrganizationViewModel Update(int id, OrganizationUpdateInputModel input);
        bool Delete(int id);
    }
}
