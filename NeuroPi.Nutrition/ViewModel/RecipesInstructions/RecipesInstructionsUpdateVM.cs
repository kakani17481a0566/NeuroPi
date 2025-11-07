using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.RecipesInstructions
{
    public class RecipesInstructionsUpdateVM
    {
        public int SequenceNumber { get; set; }

        public int NutritionalItemId { get; set; }

        public string Description { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
       
    }
}
