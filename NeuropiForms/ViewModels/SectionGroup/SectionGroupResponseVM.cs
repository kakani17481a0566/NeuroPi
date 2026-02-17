using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SectionGroup
{
    public class SectionGroupResponseVM
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        public int? SectionId { get; set; }

        public int? AppId { get; set; }

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

        public static SectionGroupResponseVM ToViewModel(MSectionGroup sectionGroup)
        {
            return new SectionGroupResponseVM
            {
                Id = sectionGroup.Id,
                GroupId = sectionGroup.GroupId,
                SectionId = sectionGroup.SectionId,
                AppId = sectionGroup.AppId,
                TenantId = sectionGroup.TenantId,
                CreatedBy = sectionGroup.CreatedBy,
                CreatedOn = sectionGroup.CreatedOn,
                UpdatedBy = sectionGroup.UpdatedBy,
                UpdatedOn = sectionGroup.UpdatedOn,
                IsDeleted = sectionGroup.IsDeleted

            };
        }

        public static List<SectionGroupResponseVM> ToViewModelList(List<MSectionGroup> sectionGroup)
        {
            return sectionGroup.Select(sg => ToViewModel(sg)).ToList();

        }
    }
}
