using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class MealPlanServiceImpl: IMealPlan
    {

        private readonly NeutritionDbContext _dbContext;

        public MealPlanServiceImpl(NeutritionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MealPlanResponseVM CreateMealPlan(MealPlanRequestVM mealplanrequestvm)
        {
            //throw new NotImplementedException();

            MMealPlan CreatMealPlan = MealPlanRequestVM.ToModel(mealplanrequestvm);
           _dbContext.MealPlan.Add(CreatMealPlan);
              _dbContext.SaveChanges();
                return MealPlanResponseVM.ToViewModel(CreatMealPlan);


        }

        public bool DeleteMealPlan(int id, int tenantid)
        {
            //throw new NotImplementedException();

            MMealPlan DeletMealPlan = _dbContext.MealPlan.FirstOrDefault(mp => mp.Id == id && mp.TenantId == tenantid && !mp.IsDeleted);
            if (DeletMealPlan == null)
            {
                return false;
            }
            DeletMealPlan.IsDeleted = true;
            _dbContext.SaveChanges();
            return true;
        }




        public List<MealPlanResponseVM> GetAllMealPlan()
        {      

            List<MMealPlan> mealplans = _dbContext.MealPlan.Where(mp => !mp.IsDeleted).ToList();
            if (mealplans != null && mealplans.Count > 0)
                {
                return MealPlanResponseVM.ToViewModelList(mealplans);               
                }
            return null;

        }

        public MealPlanResponseVM GetMealPlanById(int id)
        {
            //throw new NotImplementedException();

            MMealPlan mealplanbyid = _dbContext.MealPlan.Where(mp => mp.Id == id && !mp.IsDeleted).First();
            if (mealplanbyid != null)
            { 
                return MealPlanResponseVM.ToViewModel(mealplanbyid);
            }
            return null;

        }

        public MealPlanResponseVM GetMealPlanByIdTenantId(int id, int tenantid)
        {
            //throw new NotImplementedException();

            MMealPlan MealPlanByIdByTemamtId = _dbContext.MealPlan.FirstOrDefault(mp => mp.Id == id && mp.TenantId == tenantid && !mp.IsDeleted);
            if (MealPlanByIdByTemamtId != null )
            { 
                return MealPlanResponseVM.ToViewModel(MealPlanByIdByTemamtId);
            }
            return null;

        }

        public List<MealPlanResponseVM> GetMealPlanByTenantId(int tenantid)
        {
            //throw new NotImplementedException();

            List<MMealPlan> mealplanbytenantid = _dbContext.MealPlan.Where(mp => mp.TenantId == tenantid && !mp.IsDeleted).ToList();
            if (mealplanbytenantid == null)
            {
                return null;
            }
            return MealPlanResponseVM.ToViewModelList(mealplanbytenantid);
        }

        public MealPlanResponseVM UpdateMealPlan(int id, int tenantid, MealPlanUpdateVM mealplanrequestvm)
        {
            //throw new NotImplementedException();

            MMealPlan UpdatMealPlan = _dbContext.MealPlan.FirstOrDefault(mp => mp.Id == id && mp.TenantId == tenantid && !mp.IsDeleted);
            if (UpdatMealPlan == null)
            {
                return null;
            }
            UpdatMealPlan.UserId = mealplanrequestvm.UserId;
            UpdatMealPlan.MealTypeId = mealplanrequestvm.MealTypeId;
            UpdatMealPlan.Date = mealplanrequestvm.Date;
            UpdatMealPlan.NutritionalItemId = mealplanrequestvm.NutritionalItemId;
            UpdatMealPlan.Quantity = mealplanrequestvm.Quantity;
            UpdatMealPlan.UpdatedBy = mealplanrequestvm.UpdatedBy;
            UpdatMealPlan.UpdatedOn = mealplanrequestvm.UpdatedOn;
            _dbContext.SaveChanges();
            return MealPlanResponseVM.ToViewModel(UpdatMealPlan);

        }
    }
}
