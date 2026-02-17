using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.SectionField
{
    public class SectionFieldResponseVM
    {
        public int Id { get; set; }

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

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static SectionFieldResponseVM ToViewModel(MSectionField sectionField)
        {
            return new SectionFieldResponseVM
            {
                Id = sectionField.Id,
                SectionId = sectionField.SectionId,
                FieldId = sectionField.FieldId,
                DisplayOrder = sectionField.DisplayOrder,
                IsRequired = sectionField.IsRequired,
                CustomLabel = sectionField.CustomLabel,
                VersionId = sectionField.VersionId,
                AppId = sectionField.AppId,
                TenantId = sectionField.TenantId,
                CreatedBy = sectionField.CreatedBy,
                CreatedOn = sectionField.CreatedOn,
                UpdatedBy = sectionField.UpdatedBy,
                UpdatedOn = sectionField.UpdatedOn
            };
        }

        public static List<SectionFieldResponseVM> ToViewModelList(List<MSectionField> sectionField)
        {
            return sectionField.Select(sf => ToViewModel(sf)).ToList();
        }
    }
}