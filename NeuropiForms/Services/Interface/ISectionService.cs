using NeuropiForms.ViewModels.Sections;

namespace NeuropiForms.Services.Interface
{
    public interface ISectionService
    {
        List<SectionsResponseVM> GetAllSections();

        SectionsResponseVM GetSectionById(int id);

        SectionsResponseVM GetSectionByIdAndTenantId(int id, int tenantId);

        SectionsResponseVM GetSectionByTenantId(int tenantId);

        SectionsResponseVM CreateSection(SectionsRequestVM section);


        SectionsResponseVM UpdateSection(int id, int tenantId, SectionsUpdateVM section);


        bool DeleteSection(int id, int tenantId);


    }
}
