using NeuroPi.Nutrition.ViewModel.MealPlanMonitoring;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IMealPlanMonitoring
    {
        List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoring();

        MealPlanMonitoringResponseVM GetMealPlanMonitoringById(int id);

        MealPlanMonitoringResponseVM GetMealPlanMonitoringRequestByIdAndTenantId(int id, int tenantId);

        List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoringByTenantId(int tenantId);

        MealPlanMonitoringResponseVM CreateMealPlanMonitoring(MealPlanMonitoringRequestVM requestVM);

        MealPlanMonitoringResponseVM UpdateMealPlanMonitoring(int id, int tenantId, MealPlanMonitoringUpdateVM updateVM);

        bool DeleteMealPlanMonitoring(int id,int tenantId);
    }
}
