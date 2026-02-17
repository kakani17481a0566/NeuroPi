using NeuropiForms.Models;

namespace NeuropiForms.ViewModels.Sections
{
    public class SectionsResponseVM
    {
        public int Id { get; set; }
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

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static SectionsResponseVM ToViewModel(MSection model)
            {
                return new SectionsResponseVM
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    Parameters = model.Parameters,
                    Weightage = model.Weightage,
                    AutoWeightCal = model.AutoWeightCal,
                    MultiValues = model.MultiValues,
                    VersionId = model.VersionId,
                    Max = model.Max,
                    Min = model.Min,
                    AppId = model.AppId,
                    TenantId = model.TenantId,
                    CreatedBy = model.CreatedBy,
                    CreatedOn = model.CreatedOn,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedOn = model.UpdatedOn
                };
        }

            public static List<SectionsResponseVM> ToViewModelList(List<MSection> models)
            {
                return models.Select(m => ToViewModel(m)).ToList();
        }

    }
}
