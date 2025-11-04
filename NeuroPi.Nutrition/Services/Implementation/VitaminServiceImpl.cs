using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.Vitamins;


namespace NeuroPi.Nutrition.Services.Implementation
{

    public class VitaminServiceImpl : IVitamins
    {
        private readonly NeutritionDbContext _context;

        public VitaminServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        } 

        public VitaminsResponseVM CreateVitamin(VitaminsRequestVm vitaminRequest)
        {
            var newVitamin = VitaminsRequestVm.ToModel(vitaminRequest);
            newVitamin.CreatedOn = DateTime.UtcNow;
            _context.Vitamins.Add(newVitamin);
            _context.SaveChanges();
            return VitaminsResponseVM.ToViewModel(newVitamin);

        }

        public bool DeleteVitamin(int id, int tenantId)
        {
            var existingVitamin = _context.Vitamins.FirstOrDefault(v => v.Id == id &&v.TenantId== tenantId && !v.IsDeleted);
            if (existingVitamin != null)
            {
                existingVitamin.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<VitaminsResponseVM> GetAllVitamins()
        {
            var vitaminsList = _context.Vitamins.Where(v => !v.IsDeleted).ToList();  
            if (vitaminsList != null && vitaminsList.Count > 0)
            {
                return VitaminsResponseVM.ToViewModelList(vitaminsList);
            }
            return null;
        }

        public VitaminsResponseVM GetVitaminById(int id)
        {
            var vitamin =_context.Vitamins.FirstOrDefault(v => v.Id == id && !v.IsDeleted);
            if (vitamin != null)
            {
                return VitaminsResponseVM.ToViewModel(vitamin);
            }
            return null;
        }

        public List<VitaminsResponseVM> GetVitaminByTenantId(int tenantId)
        {
            var vitaminsList = _context.Vitamins.Where(v => v.TenantId == tenantId && !v.IsDeleted).ToList();
            if (vitaminsList != null && vitaminsList.Count > 0)
            {
                return VitaminsResponseVM.ToViewModelList(vitaminsList);
            }
            return null;
        }

        public VitaminsResponseVM GetVitaminsByIdAndTenantID(int id, int tenantId)
        {
            var vitmains = _context.Vitamins.FirstOrDefault(v => v.Id == id && v.TenantId == tenantId && !v.IsDeleted);
            if (vitmains != null)
            {
                return VitaminsResponseVM.ToViewModel(vitmains);
            }
            return null;
        }

        public VitaminsResponseVM UpdateVitamin(int id, int tenantId, VitaminsUpdateVM vitaminUpdate)
        {
            var existingVitamin = _context.Vitamins.FirstOrDefault(v => v.Id == id && v.TenantId == tenantId && !v.IsDeleted);
            if (existingVitamin != null)
            {
                existingVitamin.Code = vitaminUpdate.Code;
                existingVitamin.Name = vitaminUpdate.Name;
                existingVitamin.Description = vitaminUpdate.Description;
                existingVitamin.UpdatedBy = vitaminUpdate.UpdatedBy;
                existingVitamin.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return VitaminsResponseVM.ToViewModel(existingVitamin);
            }
            return null;

        }
    }
}
