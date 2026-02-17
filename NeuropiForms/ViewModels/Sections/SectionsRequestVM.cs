using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.Sections
{
    public class SectionsRequestVM
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

        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MSection ToModel(SectionsRequestVM vm)
        {
            return new MSection
            {
                Name = vm.Name,
                Description = vm.Description,
                IsActive = vm.IsActive,
                Parameters = vm.Parameters,
                Weightage = vm.Weightage,
                AutoWeightCal = vm.AutoWeightCal,
                MultiValues = vm.MultiValues,
                VersionId = vm.VersionId,
                Max = vm.Max,
                Min = vm.Min,
                AppId = vm.AppId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn
            };
        }   

    }
}
