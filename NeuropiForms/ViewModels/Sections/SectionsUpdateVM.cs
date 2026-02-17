namespace NeuropiForms.ViewModels.Sections
{
    public class SectionsUpdateVM
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool? IsActive { get; set; }

        public bool? Parameters { get; set; }

        public double? Weightage { get; set; }

        public bool? AutoWeightCal { get; set; }

        public bool? MultiValues { get; set; }

        public double? VersionId { get; set; }

        public int? Max { get; set; }

        public int? Min { get; set; }

        public int? AppId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
