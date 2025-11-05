using NeuroPi.Nutrition.ViewModel;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IMealPlan
    {
        //Get        
        List<MealPlanResponseVM> GetAllMealPlan();
        MealPlanResponseVM GetMealPlanById(int id);
        MealPlanResponseVM GetMealPlanByIdTenantId(int id, int tenantid);
        List<MealPlanResponseVM> GetMealPlanByTenantId(int tenantid);
       //Post
        MealPlanResponseVM CreateMealPlan(MealPlanRequestVM mealplanrequestvm);
        //put
        MealPlanResponseVM UpdateMealPlan(int id, int tenantid, MealPlanUpdateVM mealplanrequestvm);
        //delete
        bool DeleteMealPlan(int id, int tenantid);


    }
}
