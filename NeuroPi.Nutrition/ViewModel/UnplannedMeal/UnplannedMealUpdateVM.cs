namespace NeuroPi.Nutrition.ViewModel.UnplannedMeal
{
    public class UnplannedMealUpdateVM

    {
        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public string OtherName { get; set; }

        public int OtherCaloriesQuantity { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
