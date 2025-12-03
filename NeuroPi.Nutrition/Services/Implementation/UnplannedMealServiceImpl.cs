using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UnplannedMeal;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UnplannedMealServiceImpl : IUnplannedMeal
    {
        private readonly NeutritionDbContext _context;

        public UnplannedMealServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public UnplannedMealResponseVM CreateUnplannedMeal(UnplannedMealRequestVM RequestVM)
        {
            var newUnplannedMeal = UnplannedMealRequestVM.ToModel(RequestVM);
            newUnplannedMeal.CreatedOn = DateTime.UtcNow;
            _context.UnplannedMeals.Add(newUnplannedMeal);
            _context.SaveChanges();
            return UnplannedMealResponseVM.ToViewModel(newUnplannedMeal);


        }

        public bool DeleteUnplannedMeal(int id, int tenantId)
        {
            var existingmealPlan = _context.UnplannedMeals.FirstOrDefault(u => u.Id == id && u.TenantId==tenantId && !u.IsDeleted );
            if (existingmealPlan == null) 
            {
                return false;
            }
            existingmealPlan.IsDeleted = true;
            _context.SaveChanges();
            return true;

        }

        public UnplannedMealResponseVM GetUnplannedMealById(int id)
        {
            var unplannedMeal = _context.UnplannedMeals.FirstOrDefault(u=>u.Id == id && !u.IsDeleted);
            if (unplannedMeal == null) 
            {
                return null;
            }
            return UnplannedMealResponseVM.ToViewModel(unplannedMeal);
        }

        public UnplannedMealResponseVM GetUnplannedMealByIdAndTenantId(int id, int tenantId)
        {
            var unplannedMeal = _context.UnplannedMeals.FirstOrDefault(u => u.Id == id && u.TenantId== tenantId &&!u.IsDeleted);
            if (unplannedMeal == null)
            {
                return null;
            }
            return UnplannedMealResponseVM.ToViewModel(unplannedMeal);
        }

        public List<UnplannedMealResponseVM> GetUnplannedMeals()
        {
            var unplannedMeals = _context.UnplannedMeals.Where(u => !u.IsDeleted).ToList();
            if (unplannedMeals == null)
            {
                return null;
            }
            return UnplannedMealResponseVM.ToViewModelList(unplannedMeals);

        }

        public List<UnplannedMealResponseVM> GetUnplannedMealsByTenantId(int tenantId)
        {
            var unplannedMeals = _context.UnplannedMeals.Where(u =>u.TenantId==tenantId && !u.IsDeleted).ToList();
            if (unplannedMeals == null)
            {
                return null;
            }
            return UnplannedMealResponseVM.ToViewModelList(unplannedMeals);
        }

       

        public UnplannedMealResponseVM UpdateUnplannedMeal(int id, int tenantId, UnplannedMealUpdateVM UpdateVM)
        {
            var existingUnplannedMeal = _context.UnplannedMeals.FirstOrDefault(u => u.Id == id && u.TenantId== tenantId && !u.IsDeleted);
            if (existingUnplannedMeal == null)
            {
                return null;
            }
            existingUnplannedMeal.MealPlanId = UpdateVM.MealPlanId;
            existingUnplannedMeal.NutritionalItemId = UpdateVM.NutritionalItemId;
            existingUnplannedMeal.Quantity = UpdateVM.Quantity;
            existingUnplannedMeal.OtherName = UpdateVM.OtherName;
            existingUnplannedMeal.OtherCaloriesQuantity = (int)UpdateVM.OtherCaloriesQuantity;
            existingUnplannedMeal.UpdatedBy = UpdateVM.UpdatedBy;
            existingUnplannedMeal.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return UnplannedMealResponseVM.ToViewModel(existingUnplannedMeal);

        }




    }
}
