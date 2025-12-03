namespace NeuroPi.Nutrition.ViewModel.UnplannedMeal
{
    public class UnplannedMealUpdateVM
    {
        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }   // ✔ INT confirmed

        public string? OtherName { get; set; }                // ✔ Nullable string
        public decimal? OtherCaloriesQuantity { get; set; }   // ✔ Nullable decimal

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
