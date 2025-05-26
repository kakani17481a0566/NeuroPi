using SchoolManagement.ViewModel.Institutions;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IInstitutionService
    {
        // Get all institutions
        List<InstitutionResponseVM> GetAll();

        // Get institution by ID
        InstitutionResponseVM GetById(int id);

        // Create a new institution
        InstitutionResponseVM Create(InstitutionCreateRequestVM request);

        // Update an institution by ID and tenant ID
        InstitutionResponseVM UpdateByIdAndTenantId(int id, int tenantId, InstitutionUpdateRequestVM request);

        // Create a new institution along with its contact
        InstitutionWithContactResponseVM CreateWithContact(InstitutionWithContactRequestVM request);

        // Delete institution by ID and tenant ID, optionally delete contact
        bool DeleteByIdAndTenantId(int id, int tenantId, bool deleteContact);

        // Get institution and contact details by ID and tenant ID
        InstitutionWithContactResponseVM GetByIdAndTenantId(int id, int tenantId);
    }
}
