using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFavourites
{
    public class UserFavouritesRequestVM
    {
        public int UserId { get; set; }
        public int NutritionalItemId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MUserFavourites ToModel(UserFavouritesRequestVM viewModel)
        {
            return new MUserFavourites
            {
                UserId = viewModel.UserId,
                NutritionalItemId = viewModel.NutritionalItemId,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn
            };
        }
    }
}
