using NeuroPi.Nutrition.Model;
 

namespace NeuroPi.Nutrition.ViewModel.NutritionalFocus
{
    public class NutritionalFocusRequestVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public static MNutritionalFocus ToViewModel(NutritionalFocusRequestVM viewModel)
        {
            return new MNutritionalFocus
            {
                Code = viewModel.Code,
                Name = viewModel.Name,
                Description = viewModel.Description,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
   
    
    }
}
