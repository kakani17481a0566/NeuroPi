using NeuroPi.Nutrition.Model;
 

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe
{
    public class NutritionalItemRecipeRequestVM
    {
        public int NutritionalItemId { get; set; }

      
        public int RecipeItemId { get; set; }

       
        public int ItemQty { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public static MNutritionalItemRecipe ToViewModel(NutritionalItemRecipeRequestVM viewModel)
        {
            return new MNutritionalItemRecipe
            {
                NutritionalItemId = viewModel.NutritionalItemId,
                RecipeItemId = viewModel.RecipeItemId,
                ItemQty = viewModel.ItemQty,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
   
    
    }
}
