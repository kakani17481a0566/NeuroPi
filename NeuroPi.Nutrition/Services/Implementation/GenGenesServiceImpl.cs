using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.Genes;



namespace NeuroPi.Nutrition.Services.Implementation
{
    public class GenGenesServiceImpl : IGenGenesService
    {
        private readonly NeutritionDbContext _context;

        public GenGenesServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public GenGenesResponseVM CreateGenes(GenGenesRequestVM request)
        {
            var newGene = GenGenesRequestVM.ToViewModel(request);
            newGene.CreatedOn = DateTime.UtcNow;
            _context.GenGenes.Add(newGene);
            _context.SaveChanges();
            return GenGenesResponseVM.ToViewModel(newGene);
        }

        public bool DeleteGenesById(int id, int tenantId)
        {
            var existingGene = _context.GenGenes.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<GenGenesResponseVM> GetAllGenes()
        {
            var genesList = _context.GenGenes.Where(g => !g.IsDeleted).ToList();
            if (genesList != null && genesList.Count > 0)
            {
                return GenGenesResponseVM.ToViewModelList(genesList);
            }
            return null;
        }

        public GenGenesResponseVM GetGenesById(int id)
        {
            var gene = _context.GenGenes.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (gene != null)
            {
                return GenGenesResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public GenGenesResponseVM GetGenesByIdAndTenantId(int id, int tenantId)
        {
            var gene = _context.GenGenes.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return GenGenesResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public List<GenGenesResponseVM> GetGenesByTenantId(int tenantId)
        {
            var genesList = _context.GenGenes.Where(g => g.TenantId == tenantId && !g.IsDeleted).ToList();
            if (genesList != null && genesList.Count > 0)
            {
                return GenGenesResponseVM.ToViewModelList(genesList);
            }
            return null;
        }



        public GenGenesResponseVM UpdateGenesByIdAndTenantId(int id, int tenantId, GenGenesUpdateVM update)
        {
            var existingGene = _context.GenGenes.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.Code = update.Code;
                existingGene.Name = update.Name;
                existingGene.Description = update.Description;
                existingGene.UpdatedBy = update.UpdatedBy;
                existingGene.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return GenGenesResponseVM.ToViewModel(existingGene);
            }
            return null;

        }

       
    }
}
