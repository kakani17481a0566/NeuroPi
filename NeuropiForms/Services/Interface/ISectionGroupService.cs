using NeuropiForms.ViewModels.SectionGroup;

namespace NeuropiForms.Services.Interface
{
    public interface ISectionGroupService
    {
        List<SectionGroupResponseVM> GetSectionGroups();

        SectionGroupResponseVM GetById(int id);

        SectionGroupResponseVM GetByIdAndTenantId(int id, int tenantId);

        SectionGroupResponseVM GetByTenantId(int tenantId);

        SectionGroupResponseVM CreateSectionGroup(SectionGroupRequestVM request);

        SectionGroupResponseVM Update(int id, int tenantId,SectionGroupUpdateVM update);

        bool DeleteSectionGroup(int id, int tenantId);


    }
}
