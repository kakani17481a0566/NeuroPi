using NeuroPi.Nutrition.Model;
 


namespace NeuroPi.Nutrition.ViewModel
{
    public class GeneNutritionalFocusRequestVM
    {
        public int NutritionalFocusId { get; set; }

        public int GenesId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MGeneNutritionalFocus ToViewModel(GeneNutritionalFocusRequestVM viewModel) => new MGeneNutritionalFocus
           {
                NutritionalFocusId = viewModel.NutritionalFocusId,
                GenesId = viewModel.GenesId,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }

