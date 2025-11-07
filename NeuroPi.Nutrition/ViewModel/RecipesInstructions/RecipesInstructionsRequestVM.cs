using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.RecipesInstructions
{
    public class RecipesInstructionsRequestVM
    {
        public int SequenceNumber { get; set; }

        public int NutritionalItemId { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MRecipesInstructions ToModel(RecipesInstructionsRequestVM model)
        {
            return new MRecipesInstructions
            { 
                SequenceNumber = model.SequenceNumber,
                NutritionalItemId = model.NutritionalItemId,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn

            };

        }
    }
}
