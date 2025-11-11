using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    public class MealPlanMonitoringRequestVM
    {
        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MMealPlanMonitoring ToModel(MealPlanMonitoringRequestVM vm)
        {
            return new MMealPlanMonitoring
            {
                MealPlanId = vm.MealPlanId,
                NutritionalItemId = vm.NutritionalItemId,
                Quantity = vm.Quantity,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn,
            };

        }
    }
}
