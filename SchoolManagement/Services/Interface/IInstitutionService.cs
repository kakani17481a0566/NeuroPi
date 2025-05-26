using SchoolManagement.ViewModel.Institutions;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IInstitutionService
    {
        List<InstitutionResponseVM> GetAll();
        InstitutionResponseVM GetById(int id);
        InstitutionResponseVM Create(InstitutionCreateRequestVM request);
        InstitutionResponseVM UpdateByIdAndTenantId(int id, int tenantId, InstitutionUpdateRequestVM request);

        InstitutionWithContactResponseVM CreateWithContact(InstitutionWithContactRequestVM request);






        bool Delete(int id);


        InstitutionResponseVM GetByIdAndTenantId(int id, int tenantId);

    }
}
