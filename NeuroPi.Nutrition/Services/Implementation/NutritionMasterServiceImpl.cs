using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionMaster;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionMasterServiceImpl : INutritionMaster
    {
        private readonly NeutritionDbContext _Context;

        public NutritionMasterServiceImpl(NeutritionDbContext context)
        {
            _Context = context;
        }

        public NutritionMasterResponseVM CreateNutritionMaster(NutritionMasterRequestVM request)
        {
            var newNutritionMaster = NutritionMasterRequestVM.ToModel(request);
            newNutritionMaster.CreatedOn = DateTime.UtcNow;
            _Context.NutritionMasters.Add(newNutritionMaster);
            _Context.SaveChanges();
            return NutritionMasterResponseVM.ToViewModel(newNutritionMaster);
        }

        public bool DeleteNutritionMaster(int id, int tenantId)
        {
            var existingNutritionMaster = _Context.NutritionMasters
                .FirstOrDefault(nm => nm.Id == id && nm.TenantId == tenantId);
            if (existingNutritionMaster == null)
                {
                return false;
            }
            existingNutritionMaster.IsDeleted = true;
            _Context.SaveChanges();
            return true;


        }

        public List<NutritionMasterResponseVM> GetNutritionMaster()
        {
            var nutritionMasterList = _Context.NutritionMasters
                .Where(nm => !nm.IsDeleted)
                .ToList();
            if (nutritionMasterList != null && nutritionMasterList.Count > 0)
                {
                return NutritionMasterResponseVM.ToViewModelList(nutritionMasterList);
            }
            return null;
        }
            
        public NutritionMasterResponseVM GetNutritionMasterById(int id)
        {
            var nutritionMaster = _Context.NutritionMasters
                .FirstOrDefault(nm => nm.Id == id && !nm.IsDeleted);
            if (nutritionMaster != null)
            {

                return NutritionMasterResponseVM.ToViewModel(nutritionMaster);
            }
            return null;
        }

        public NutritionMasterResponseVM GetNutritionMasterByIdAndTenantId(int id, int tenantId)
        {
            var nutritionMaster = _Context.NutritionMasters
                .FirstOrDefault(nm => nm.Id == id && nm.TenantId == tenantId && !nm.IsDeleted);
            if (nutritionMaster != null)
            {
                return NutritionMasterResponseVM.ToViewModel(nutritionMaster);
            }
            return null;
        }

        public List<NutritionMasterResponseVM> GetNutritionMasterByTenantId(int tenantId)
        {
            var nutritionMasterList = _Context.NutritionMasters
                .Where(nm => nm.TenantId == tenantId && !nm.IsDeleted)
                .ToList();
            if (nutritionMasterList != null && nutritionMasterList.Count > 0)
            {
                return NutritionMasterResponseVM.ToViewModelList(nutritionMasterList);
            }
            return null;
        }

        public NutritionMasterResponseVM UpdateNutritionMaster(int id, int tenantId, NutritionMasterUpdateVM request)
        {
            var existingNutritionMaster = _Context.NutritionMasters
                .FirstOrDefault(nm => nm.Id == id && nm.TenantId == tenantId && !nm.IsDeleted);
            if (existingNutritionMaster == null)
            {
                return null;
            }
            existingNutritionMaster.Name = request.name;
            existingNutritionMaster.NutritionMasterTypeId = request.NutritionMasterTypeId;
            existingNutritionMaster.UpdatedBy = request.UpdatedBy;
            existingNutritionMaster.UpdatedOn = DateTime.UtcNow;
            _Context.SaveChanges();
            return NutritionMasterResponseVM.ToViewModel(existingNutritionMaster);
        }
    }
}
