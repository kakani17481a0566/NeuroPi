using NeuroPi.Nutrition.Model;
 

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe
{
    public class NutritionalItemRecipeResponseVM
    {
        public int Id { get; set; }
        public int NutritionalItemId { get; set; }


        public int RecipeItemId { get; set; }


        public int ItemQty { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public static NutritionalItemRecipeResponseVM ToViewModel(MNutritionalItemRecipe model)
        {
            return new NutritionalItemRecipeResponseVM
            {
                Id = model.Id,
                NutritionalItemId = model.NutritionalItemId,
                RecipeItemId = model.RecipeItemId,
                ItemQty = model.ItemQty,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<NutritionalItemRecipeResponseVM> ToViewModelList(List<MNutritionalItemRecipe> modelList)
        {
            List<NutritionalItemRecipeResponseVM> result = new List<NutritionalItemRecipeResponseVM>();
            foreach (var model in modelList)
            {
                result.Add(ToViewModel(model));
            }
            ;
            return result;
        }
   
    }
}
