namespace NeuroPi.Nutrition.ViewModel
{
    public class MealPlanUpdateVM
    {
        public int UserId { get; set; }
        public int MealTypeId { get; set; }
        public DateOnly Date { get; set; }
        public int NutritionalItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.Now;
        public int? UpdatedBy { get; set; }

    }
}
