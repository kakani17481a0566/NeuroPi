using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UnplannedMeal
{
    public class UnplannedMealRequestVM
    {
        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public string OtherName { get; set; }

        public int OtherCaloriesQuantity { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MUnplannedMeal ToModel(UnplannedMealRequestVM viewModel)
        {
            return new MUnplannedMeal
            {
                MealPlanId = viewModel.MealPlanId,
                NutritionalItemId = viewModel.NutritionalItemId,
                Quantity = viewModel.Quantity,
                OtherName = viewModel.OtherName,
                OtherCaloriesQuantity = viewModel.OtherCaloriesQuantity,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn
            };
        }
    }
}
