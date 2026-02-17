using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SectionGroup
{
    public class SectionGroupRequestVM
    {
        public int? GroupId { get; set; }

        public int? SectionId { get; set; }

        public int? AppId { get; set; }

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

       public static MSectionGroup ToModel(SectionGroupRequestVM model)
        {
            return new MSectionGroup
            {
                GroupId = model.GroupId,
                SectionId = model.SectionId,
                AppId = model.AppId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn
            };
        }
    }
}
