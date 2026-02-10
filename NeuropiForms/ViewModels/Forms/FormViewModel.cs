using System;

namespace NeuropiForms.ViewModels.Forms
{
    public class FormViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? ActiveVersion { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsPublished { get; set; }
        public double? VersionId { get; set; }
        public int? MaxVersions { get; set; }
        public int? ComplintensId { get; set; }
        public int? StorageTypeId { get; set; }
        public string StorageId { get; set; }
        public int? AppId { get; set; }
        public int? TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
