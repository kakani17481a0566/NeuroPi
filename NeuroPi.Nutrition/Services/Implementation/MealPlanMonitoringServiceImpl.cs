using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.MealPlanMonitoring;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class MealPlanMonitoringServiceImpl : IMealPlanMonitoring
    {
        private readonly NeutritionDbContext _context;

        public MealPlanMonitoringServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public MealPlanMonitoringResponseVM CreateMealPlanMonitoring(MealPlanMonitoringRequestVM requestVM)
        {
            var newMPMonitoring = MealPlanMonitoringRequestVM.ToModel(requestVM);
            newMPMonitoring.CreatedOn = requestVM.CreatedOn;
            _context.MealPlanMonitoring.Add(newMPMonitoring);
            _context.SaveChanges();
            return MealPlanMonitoringResponseVM.ToViewModel(newMPMonitoring);
        }

        public bool DeleteMealPlanMonitoring(int id, int tenantId)
        {
            var existingMPM = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (existingMPM == null)
            { 
                return false;
            }
            existingMPM.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoring()
        {
            var mPMList = _context.MealPlanMonitoring.Where(m=> !m.IsDeleted).ToList();
            if(mPMList != null)
            {
                return MealPlanMonitoringResponseVM.ToViewModelList(mPMList);
            }
            return null;
            
        }

        public List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoringByTenantId(int tenantId)
        {
            var mealPlanMonitoring =_context.MealPlanMonitoring.Where(m => m.TenantId == tenantId && !m.IsDeleted).ToList();
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModelList(mealPlanMonitoring);

        }

        public MealPlanMonitoringResponseVM GetMealPlanMonitoringById(int id)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);

        }

        public MealPlanMonitoringResponseVM GetMealPlanMonitoringRequestByIdAndTenantId(int id, int tenantId)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId== tenantId && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);
        }

        public MealPlanMonitoringResponseVM UpdateMealPlanMonitoring(int id, int tenantId, MealPlanMonitoringUpdateVM updateVM)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            mealPlanMonitoring.MealPlanId = updateVM.MealPlanId;
            mealPlanMonitoring.NutritionalItemId = updateVM.NutritionalItemId;
            mealPlanMonitoring.Quantity = updateVM.Quantity;
            mealPlanMonitoring.UpdatedBy = updateVM.UpdatedBy;
            mealPlanMonitoring.UpdatedOn = updateVM.UpdatedOn;

            _context.SaveChanges();
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);
        }
    }
}
