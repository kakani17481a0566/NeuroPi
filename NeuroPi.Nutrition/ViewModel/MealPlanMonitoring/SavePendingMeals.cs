namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    public class SavePendingMeals
    {
        public string Date { get; set; }                     // yyyy-MM-dd

        public int MealTypeId { get; set; }                  // Breakfast, Lunch etc.

        public int NutritionalItemId { get; set; }           // 0 → custom item

        public int PlannedQty { get; set; }                  // Qty from meal plan
        public int ConsumedQty { get; set; }                 // User entered qty

        // Optional only for custom foods (when NutritionalItemId = 0)
        public string? OtherName { get; set; }               // Non-database item
        public int OtherCaloriesQuantity { get; set; }       // kcal for OtherName items
    }

    public class SaveMealsTrackingResponseVM
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
