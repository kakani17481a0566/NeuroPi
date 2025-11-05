using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;

 



namespace NeuroPi.Nutrition.Services.Implementation
{
    public class GeneNutritionalFocusServiceImpl :IGeneNutritionalFocus
    {
        private readonly NeutritionDbContext _context;

        public GeneNutritionalFocusServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public GeneNutritionalFocusResponseVM CreateGeneNutritionalFocus(GeneNutritionalFocusRequestVM request)
        {
            
            var newModel = GeneNutritionalFocusRequestVM.ToViewModel(request);
            newModel.CreatedOn = DateTime.UtcNow;
            _context.GeneNutritionalFocus.Add(newModel);
            _context.SaveChanges();             
            
            return GeneNutritionalFocusResponseVM.ToViewModel(newModel);
        }

        public bool DeleteGeneNutritionalFocusById(int id, int tenantId)
        {
            var existingGene = _context.GeneNutritionalFocus.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GeneNutritionalFocusResponseVM> GetAllGeneNutritionalFocus()
        {
            var GeneNutritionalFocusList = _context.GeneNutritionalFocus.Where(g => !g.IsDeleted).ToList();
            if (GeneNutritionalFocusList != null && GeneNutritionalFocusList.Count > 0)
            {
                return GeneNutritionalFocusResponseVM.ToViewModelList(GeneNutritionalFocusList);
            }
            return null;
        }

        //public GeneNutritionalFocusResponseVM GetGeneIdById(int id)
        //{
        //    var GeneNutritionalFocusList = _context.GeneNutritionalFocus.Where(g =>g.Id==id  && !g.IsDeleted);
        //    if (GeneNutritionalFocusList != null )
        //    {
        //        return GeneNutritionalFocusResponseVM.ToViewModel(GeneNutritionalFocusList);
        //    }
        //    return null;
        //}
        //public GeneNutritionalFocusResponseVM GetNutritionalFocusById(int id)
        //{
        //    var GeneNutritionalFocusList = _context.GeneNutritionalFocus.FirstOrDefault(g =>g.Id==id && !g.IsDeleted);
        //    if (GeneNutritionalFocusList != null )
        //    {
        //        return GeneNutritionalFocusResponseVM.ToViewModel(GeneNutritionalFocusList);
        //    }
        //    return null;
        //}


        public GeneNutritionalFocusResponseVM GetGeneNutritionalFocusById(int id)
        {
            var gene = _context.GeneNutritionalFocus.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (gene != null)
            {
                return GeneNutritionalFocusResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public GeneNutritionalFocusResponseVM GetGeneNutritionalFocusByIdAndTenantId(int id, int tenantId)
        {
            var gene = _context.GeneNutritionalFocus.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return GeneNutritionalFocusResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public List<GeneNutritionalFocusResponseVM> GetGeneNutritionalFocusByTenantId(int tenantId)
        {
            var gene = _context.GeneNutritionalFocus.Where(g => g.TenantId == tenantId && !g.IsDeleted).ToList();
            if (gene != null)
            {
                return GeneNutritionalFocusResponseVM.ToViewModelList(gene);
            }
            return null;
        }



        public GeneNutritionalFocusResponseVM UpdateGeneNutritionalFocusByIdAndTenantId(int id, int tenantId, GeneNutritionalFocusUpdateVM update)
        {
            var existingGene = _context.GeneNutritionalFocus.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.NutritionalFocusId = update.NutritionalFocusId;
                existingGene.GenesId = update.GenesId;
               
                existingGene.UpdatedBy = update.UpdatedBy;
                existingGene.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return GeneNutritionalFocusResponseVM.ToViewModel(existingGene);
            }
            return null;

        }


    }
}
