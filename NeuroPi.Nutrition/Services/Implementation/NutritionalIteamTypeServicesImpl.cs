
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Interface;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalIteamType;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalIteamTypeServicesImpl :INutritionalIteamType
    {
        private readonly NeutritionDbContext _context;

        public NutritionalIteamTypeServicesImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public NutritionalIteamTypeResponseVM CreateNutritionalIteamType(NutritionalIteamTypeRequestVM request)
        {
            var newModel = NutritionalIteamTypeRequestVM.ToViewModel(request);
            newModel.CreatedOn = DateTime.UtcNow;
            _context.NutritionalIteamType.Add(newModel);
            _context.SaveChanges();
            return NutritionalIteamTypeResponseVM.ToViewModel(newModel);
        }

        public bool DeleteNutritionalIteamTypeById(int id, int tenantId)
        {
            var existingGene = _context.NutritionalIteamType.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<NutritionalIteamTypeResponseVM> GetAllNutritionalIteamType()
        {
            var NutritionalIteamTypeList = _context.NutritionalIteamType.Where(g => !g.IsDeleted).ToList();
            if (NutritionalIteamTypeList != null && NutritionalIteamTypeList.Count > 0)
            {
                return NutritionalIteamTypeResponseVM.ToViewModelList(NutritionalIteamTypeList);
            }
            return new List<NutritionalIteamTypeResponseVM>();
        }

        public NutritionalIteamTypeResponseVM GetNutritionalIteamTypeById(int id)
        {
            var gene = _context.NutritionalIteamType.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalIteamTypeResponseVM.ToViewModel(gene);
            }
            return new NutritionalIteamTypeResponseVM();
        }

        public NutritionalIteamTypeResponseVM GetNutritionalIteamTypeByIdAndTenantId(int id, int tenantId)
        {
            var gene = _context.NutritionalIteamType.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalIteamTypeResponseVM.ToViewModel(gene);
            }
            return new NutritionalIteamTypeResponseVM();
        }

        public NutritionalIteamTypeResponseVM GetNutritionalIteamTypeByTenantId(int tenantId)
        {
            var gene = _context.NutritionalIteamType.FirstOrDefault(g => g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalIteamTypeResponseVM.ToViewModel(gene);
            }
            return new NutritionalIteamTypeResponseVM();
        }



        public NutritionalIteamTypeResponseVM UpdateNutritionalIteamTypeByIdAndTenantId(int id, int tenantId, NutritionalIteamTypeUpdateVM update)
        {
            var existingGene = _context.NutritionalIteamType.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.Code = update.Code;
                existingGene.Name = update.Name;
                
                existingGene.UpdatedBy = update.UpdatedBy;
                existingGene.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return NutritionalIteamTypeResponseVM.ToViewModel(existingGene);
            }
            return new NutritionalIteamTypeResponseVM();

        }


    }
}
