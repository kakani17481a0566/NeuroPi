namespace NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe
{
    public class NutritionalItemRecipeUpdateVM
    {
        public int NutritionalItemId { get; set; }


        public int RecipeItemId { get; set; }


        public int ItemQty { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
