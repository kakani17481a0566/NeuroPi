using NeuropiForms.ViewModels.SectionField;

namespace NeuropiForms.Services.Interface
{
    public interface ISectionFieldService
    {
        List<SectionFieldResponseVM> GetAllSectionFields();

        SectionFieldResponseVM GetById(int id);

        SectionFieldResponseVM GetByIdAndTenantId(int id, int tenantId);

        SectionFieldResponseVM GetByTenantId(int tenantId);

        SectionFieldResponseVM CreateSectionField(SectionFieldRequestVM requestVM);

        SectionFieldResponseVM UpdateSectionField(int id, int tenantId, SectionFieldUpdateVM updateVM);

        bool DeleteSectionField(int id, int tenantId);
    }
}
