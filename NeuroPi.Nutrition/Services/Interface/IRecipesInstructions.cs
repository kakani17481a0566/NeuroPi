using NeuroPi.Nutrition.ViewModel.RecipesInstructions;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IRecipesInstructions
    {
        List<RecipesInstructionsResponseVM> GetRecipesInstructions();

        RecipesInstructionsResponseVM GetRecipesInstructionsById(int id);

        RecipesInstructionsResponseVM GetRecipesInstructionsByIdAndTenantId(int id, int tenantId);

        List<RecipesInstructionsResponseVM> GetRecipesInstructionsByTenantId(int tenantId);

        RecipesInstructionsResponseVM CreateRecipesInstruction(RecipesInstructionsRequestVM request);

        RecipesInstructionsResponseVM UpdateRecipesInstruction(int id, int tenantId, RecipesInstructionsUpdateVM request);

        bool DeleteRecipesInstruction(int id, int tenantId);


    }
}
