namespace NeuroPi.Nutrition.ViewModel.NutritionalItemMealType
{
    public class NutritionalItemMealTypeUpdateVM
    {
        public int NutritionalItemId { get; set; }

        public int MealTypeId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
