using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalItemVitaminsServiceImpl : INutritionalItemVitamins
    {
        private readonly NeutritionDbContext _context;

        public NutritionalItemVitaminsServiceImpl (NeutritionDbContext context)
        {
            _context = context;
        }

        public List<NutritionalItemVitaminsResponseVM> GetNutritionalItemVitamins()
        {
            var NutritionalItemVits = _context.NutritionalItemVitamins.Where(v => !v.IsDeleted).ToList();
            if (NutritionalItemVits == null)
            {
                return null;
            }
            return NutritionalItemVitaminsResponseVM.ToViewModelList(NutritionalItemVits);
        }

        public NutritionalItemVitaminsResponseVM GetNutritionalItemVitaminsById(int id)
        {
            var NutritionItemVits = _context.NutritionalItemVitamins.FirstOrDefault(v => v.Id == id && !v.IsDeleted);
            if (NutritionItemVits == null)
            {
                return null;
            }
            return NutritionalItemVitaminsResponseVM.ToViewModel(NutritionItemVits);
        }

        public NutritionalItemVitaminsResponseVM GetNutritionalItemVitaminsByIdAndTenantId(int id, int tenantId)
        {
            var NutritionItemVits = _context.NutritionalItemVitamins.FirstOrDefault(v => v.Id == id && v.TenantId==tenantId &&!v.IsDeleted);
            if (NutritionItemVits == null)
            {
                return null;
            }
            return NutritionalItemVitaminsResponseVM.ToViewModel(NutritionItemVits);

        }

        public List<NutritionalItemVitaminsResponseVM> GetNutritionalVitaminByTenantId(int tenantId)
        {
            var NutritionItemVitsList = _context.NutritionalItemVitamins.Where(v => v.TenantId == tenantId && !v.IsDeleted).ToList();
            if (NutritionItemVitsList == null)
            {
                return null;
            }
            return NutritionalItemVitaminsResponseVM.ToViewModelList(NutritionItemVitsList);
        }
        public NutritionalItemVitaminsResponseVM CreateNutritionalItemVitamins(NutritionalItemVitaminsRequestVM request)
        {
            var newNutritionalItemVits = NutritionalItemVitaminsRequestVM.ToModel(request);
            newNutritionalItemVits.CreatedOn = DateTime.UtcNow;
            _context.NutritionalItemVitamins.Add(newNutritionalItemVits);
            _context.SaveChanges();
            return NutritionalItemVitaminsResponseVM.ToViewModel(newNutritionalItemVits);
        }

        public NutritionalItemVitaminsResponseVM UpdateNutritionalItemVitamins(int id, int tenantId, NutritionalItemVitaminsUpdateVM request)
        {
            var existingNutritionalItemVits = _context.NutritionalItemVitamins.FirstOrDefault(v => v.Id == id && v.TenantId == tenantId && !v.IsDeleted);
            if (existingNutritionalItemVits != null)
            {
                existingNutritionalItemVits.NutritionalItemId = request.NutritionalItemId;
                existingNutritionalItemVits.VitaminsId = request.VitaminsId;
                existingNutritionalItemVits.UpdatedBy = request.UpdatedBy;
                existingNutritionalItemVits.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return NutritionalItemVitaminsResponseVM.ToViewModel(existingNutritionalItemVits);
            }
            return null;

        }

        public bool DeleteNutritionalItemVitamins(int id, int tenantId)
        {
            var existingNutritionalItemVits = _context.NutritionalItemVitamins.FirstOrDefault(v => v.Id == id && v.TenantId == tenantId && !v.IsDeleted);
            if (existingNutritionalItemVits == null)
            {
                return false;
            }     
            existingNutritionalItemVits.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
