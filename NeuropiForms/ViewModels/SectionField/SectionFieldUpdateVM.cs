namespace NeuropiForms.ViewModels.SectionField
{
    public class SectionFieldUpdateVM
    {
        public int? SectionId { get; set; }

        public int? FieldId { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? IsRequired { get; set; }

        public string CustomLabel { get; set; }

        public double? VersionId { get; set; }

        public int? AppId { get; set; }

        public int? TenantId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
