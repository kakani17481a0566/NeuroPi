using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Interface; 
using NeuroPi.Nutrition.ViewModel.NutritionalFocus;
 



namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalFocusServiceImpl:INutritionalFocus
    {
        private readonly NeutritionDbContext _context;

        public NutritionalFocusServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public NutritionalFocusResponseVM CreateNutritionalFocus(NutritionalFocusRequestVM request)
        {
            var newModel = NutritionalFocusRequestVM.ToViewModel(request);
            newModel.CreatedOn = DateTime.UtcNow;
            _context.NutritionalFocuses.Add(newModel);
            _context.SaveChanges();
            return NutritionalFocusResponseVM.ToViewModel(newModel);
        }

        public bool DeleteNutritionalFocusById(int id, int tenantId)
        {
            var existingGene = _context.NutritionalFocuses.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<NutritionalFocusResponseVM> GetAllNutritionalFocus()
        {
            var NutritionalFocusList = _context.NutritionalFocuses.Where(g => !g.IsDeleted).ToList();
            if (NutritionalFocusList != null && NutritionalFocusList.Count > 0)
            {
                return NutritionalFocusResponseVM.ToViewModelList(NutritionalFocusList);
            }
            return null;
        }

        public NutritionalFocusResponseVM GetNutritionalFocusById(int id)
        {
            var gene = _context.NutritionalFocuses.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalFocusResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public NutritionalFocusResponseVM GetNutritionalFocusByIdAndTenantId(int id, int tenantId)
        {
            var gene = _context.NutritionalFocuses.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalFocusResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public List<NutritionalFocusResponseVM> GetNutritionalFocusByTenantId(int tenantId)
        {
            var gene = _context.NutritionalFocuses.Where(g => g.TenantId == tenantId && !g.IsDeleted).ToList();
            if (gene != null)
            {
                return NutritionalFocusResponseVM.ToViewModelList(gene);
            }
            return null;
        }



        public NutritionalFocusResponseVM UpdateNutritionalFocusByIdAndTenantId(int id, int tenantId, NutritionalFocusUpdateVM update)
        {
            var existingGene = _context.NutritionalFocuses.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.Code = update.Code;
                existingGene.Name = update.Name;
                existingGene.Description = update.Description;
                existingGene.UpdatedBy = update.UpdatedBy;
                existingGene.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return NutritionalFocusResponseVM.ToViewModel(existingGene);
            }
            return null;

        }


    }
}
