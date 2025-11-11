using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe;




namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalItemRecipeServiceImpl : INutritionalItemRecipe
    {
        private readonly NeutritionDbContext _context;

        public NutritionalItemRecipeServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public NutritionalItemRecipeResponseVM CreateNutritionalItemRecipe(NutritionalItemRecipeRequestVM request)
        {
            var newModel = NutritionalItemRecipeRequestVM.ToViewModel(request);
            newModel.CreatedOn = DateTime.UtcNow;
            _context.NutritionalItemRecipe.Add(newModel);
            _context.SaveChanges();
            return NutritionalItemRecipeResponseVM.ToViewModel(newModel);
        }

        public bool DeleteNutritionalItemRecipeById(int id, int tenantId)
        {
            var existingGene = _context.NutritionalItemRecipe.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<NutritionalItemRecipeResponseVM> GetAllNutritionalItemRecipe()
        {
            var NutritionalItemRecipeList = _context.NutritionalItemRecipe.Where(g => !g.IsDeleted).ToList();
            if (NutritionalItemRecipeList != null && NutritionalItemRecipeList.Count > 0)
            {
                return NutritionalItemRecipeResponseVM.ToViewModelList(NutritionalItemRecipeList);
            }
            return null;
        }

        public NutritionalItemRecipeResponseVM GetNutritionalItemRecipeById(int id)
        {
            var gene = _context.NutritionalItemRecipe.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalItemRecipeResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public NutritionalItemRecipeResponseVM GetNutritionalItemRecipeByIdAndTenantId(int id, int tenantId)
        {
            var gene = _context.NutritionalItemRecipe.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (gene != null)
            {
                return NutritionalItemRecipeResponseVM.ToViewModel(gene);
            }
            return null;
        }

        public List<NutritionalItemRecipeResponseVM> GetNutritionalItemRecipeByTenantId(int tenantId)
        {
            var gene = _context.NutritionalItemRecipe.Where(g => g.TenantId == tenantId && !g.IsDeleted).ToList();
            if (gene != null)
            {
                return NutritionalItemRecipeResponseVM.ToViewModelList(gene);
            }
            return null;
        }



        public NutritionalItemRecipeResponseVM UpdateNutritionalItemRecipeByIdAndTenantId(int id, int tenantId, NutritionalItemRecipeUpdateVM update)
        {
            var existingGene = _context.NutritionalItemRecipe.FirstOrDefault(g => g.Id == id && g.TenantId == tenantId && !g.IsDeleted);
            if (existingGene != null)
            {
                existingGene.NutritionalItemId = update.NutritionalItemId;
                existingGene.RecipeItemId = update.RecipeItemId;
                existingGene.ItemQty = update.ItemQty;
                existingGene.UpdatedBy = update.UpdatedBy;
                existingGene.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return NutritionalItemRecipeResponseVM.ToViewModel(existingGene);
            }
            return null;

        }


    }
}
