using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionMasterType
{
    public class NutritionMasterTypeResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static NutritionMasterTypeResponseVM ToViewModel(MNutritionMasterType model)
        {
            return new NutritionMasterTypeResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn,
            };
        }

        public static List<NutritionMasterTypeResponseVM> ToViewModelList(List<MNutritionMasterType> model)
        {
            return model.Select(m => ToViewModel(m)).ToList();
        }
    }
}
