namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    public class MealPlanMonitoringUpdateVM
    {
        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
