using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionMaster
{
    public class NutritionMasterRequestVM
    {
        public string Name { get; set; }

        public int NutritionMasterTypeId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MNutritionMaster ToModel(NutritionMasterRequestVM viewModel)
        {
            return new MNutritionMaster
            {
                Name = viewModel.Name,
                NutritionMasterTypeId = viewModel.NutritionMasterTypeId,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn
            };
        }
    }
}
