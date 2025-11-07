using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemMealType
{
    public class NutritionalItemMealTypeRequestVM
    {
        public int NutritionalItemId { get; set; }

        public int MealTypeId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MNutritionalItemMealType ToModel(NutritionalItemMealTypeRequestVM vm)
        {
            return new MNutritionalItemMealType 
            {
                NutritionalItemId = vm.NutritionalItemId,
                MealTypeId = vm.MealTypeId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn

            };

        }
    }
}
