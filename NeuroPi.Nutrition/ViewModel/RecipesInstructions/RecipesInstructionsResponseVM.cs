using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.RecipesInstructions
{
    public class RecipesInstructionsResponseVM
    {
        public int Id { get; set; }

        public int SequenceNumber { get; set; }

        public int NutritionalItemId { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn  { get; set; }


        public static RecipesInstructionsResponseVM ToViewModel(MRecipesInstructions model)
        {
            return new RecipesInstructionsResponseVM
            {
                Id = model.Id,
                SequenceNumber = model.SequenceNumber,
                NutritionalItemId = model.NutritionalItemId,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }
        public static List<RecipesInstructionsResponseVM> ToViewModelList(List<MRecipesInstructions> modelList)
        {
            return modelList.Select(model => ToViewModel(model)).ToList();
        }

    }
}
