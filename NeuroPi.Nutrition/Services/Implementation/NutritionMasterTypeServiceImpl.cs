using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionMasterType;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionMasterTypeServiceImpl : INutritionMasterType
    {
        private readonly NeutritionDbContext _context;

        public NutritionMasterTypeServiceImpl(NeutritionDbContext context)
        { 
            _context = context;
        
        }

        public NutritionMasterTypeResponseVM CreateNutritionMasterType(NutritionMasterTypeRequestVM RequestVM)
        {
            var newNutritionMasterType = NutritionMasterTypeRequestVM.ToModel(RequestVM);
            newNutritionMasterType.CreatedOn = DateTime.UtcNow;
            _context.NutritionMasterTypes.Add(newNutritionMasterType);
            _context.SaveChanges();
            return NutritionMasterTypeResponseVM.ToViewModel(newNutritionMasterType);
        }

        public bool DeleteNutritionMasterType(int id, int tenantId)
        {
            var existingNutritionMasterType = _context.NutritionMasterTypes.FirstOrDefault(n => n.Id == id && n.TenantId == tenantId && !n.IsDeleted);
            if (existingNutritionMasterType != null)
            {
                existingNutritionMasterType.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public NutritionMasterTypeResponseVM GetAllNutritionMasterTypeById(int id)
        {
            var nutritionMasterType = _context.NutritionMasterTypes.FirstOrDefault(n => n.Id == id && !n.IsDeleted);
            if (nutritionMasterType != null)
            {
                return NutritionMasterTypeResponseVM.ToViewModel(nutritionMasterType);
            }
            return null;
        }

        public NutritionMasterTypeResponseVM GetAllNutritionMasterTypeByIdAndTenantId(int id, int tenantId)
        {
            var nutritionMasterType = _context.NutritionMasterTypes.FirstOrDefault(n => n.Id == id && n.TenantId == tenantId && !n.IsDeleted);
            if (nutritionMasterType != null)
            {
                return NutritionMasterTypeResponseVM.ToViewModel(nutritionMasterType);
            }
            return null;
        }

        public List<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypeByTenantId(int tenantId)
        {
            var nutritionMasterTypeList = _context.NutritionMasterTypes.Where(n => n.TenantId == tenantId && !n.IsDeleted).ToList();
            if (nutritionMasterTypeList != null && nutritionMasterTypeList.Count > 0)
            {
                return NutritionMasterTypeResponseVM.ToViewModelList(nutritionMasterTypeList);
            }
            return null;
        }

        public List<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypes()
        {
            var nutritionMasterTypeList = _context.NutritionMasterTypes.Where(n => !n.IsDeleted).ToList();
            if (nutritionMasterTypeList != null && nutritionMasterTypeList.Count > 0)
            {
                return NutritionMasterTypeResponseVM.ToViewModelList(nutritionMasterTypeList);
            }
            return null;
        }

        public NutritionMasterTypeResponseVM UpdateNutritionMasterType(int id, int tenantId, NutritionMasterTypeUpdateVM RequestVM)
        {
            var existingNutritionMasterType = _context.NutritionMasterTypes.FirstOrDefault(n => n.Id == id && n.TenantId == tenantId && !n.IsDeleted);
            if (existingNutritionMasterType != null)
            {
                existingNutritionMasterType.Name = RequestVM.Name;
                existingNutritionMasterType.UpdatedBy = RequestVM.UpdatedBy;
                existingNutritionMasterType.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return NutritionMasterTypeResponseVM.ToViewModel(existingNutritionMasterType);
            }
            return null;
        }
    }
}
