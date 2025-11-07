using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins
{
    public class NutritionalItemVitaminsRequestVM
    {
        public int NutritionalItemId { get; set; }
        public int VitaminsId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MNutritionalItemVitamins ToModel(NutritionalItemVitaminsRequestVM viewModel)
        {
            return new MNutritionalItemVitamins
            {
                NutritionalItemId = viewModel.NutritionalItemId,
                VitaminsId = viewModel.VitaminsId,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn
            };
        }
    }
}
