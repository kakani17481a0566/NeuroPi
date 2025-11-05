using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionMaster
{
    public class NutritionMasterResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NutritionMasterTypeId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static NutritionMasterResponseVM ToViewModel(MNutritionMaster model)
        {
            return new NutritionMasterResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                NutritionMasterTypeId = model.NutritionMasterTypeId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<NutritionMasterResponseVM> ToViewModelList(List<MNutritionMaster> models)
        {
            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
