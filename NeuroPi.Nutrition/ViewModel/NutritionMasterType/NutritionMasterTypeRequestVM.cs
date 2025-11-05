using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionMasterType
{
    public class NutritionMasterTypeRequestVM
    {
        public string Name { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }


        public static MNutritionMasterType ToModel(NutritionMasterTypeRequestVM request)
        {
            return new MNutritionMasterType()
            {
                Name = request.Name,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };
                
        }
    }
}
