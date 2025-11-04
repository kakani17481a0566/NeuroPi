using NeuroPi.Nutrition.Model;
 
namespace NeuroPi.Nutrition.ViewModel.NutritionalIteamType
{
    public class NutritionalIteamTypeRequestVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
         
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public static MNutritionalIteamType ToViewModel(NutritionalIteamTypeRequestVM viewModel)=> new MNutritionalIteamType
        {
            
                Code = viewModel.Code,
                Name = viewModel.Name,
                
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        


    }
}
