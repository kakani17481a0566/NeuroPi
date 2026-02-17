using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SectionField
{
    public class SectionFieldRequestVM
    {
        public int? SectionId { get; set; }

        public int? FieldId { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsRequired { get; set; }

        public string CustomLabel { get; set; }

        public double? VersionId { get; set; }

        public int? AppId { get; set; }

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MSectionField ToModel(SectionFieldRequestVM request)
        {
            return new MSectionField
            {
                SectionId = request.SectionId,
                FieldId = request.FieldId,
                DisplayOrder = request.DisplayOrder,
                IsRequired = request.IsRequired,
                CustomLabel = request.CustomLabel,
                VersionId = request.VersionId,
                AppId = request.AppId,
                TenantId = request.TenantId
            };
        }
            
    }
}
