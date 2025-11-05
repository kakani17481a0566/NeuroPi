using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserFavourites
{
    public class UserFavouritesResponseVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NutritionalItemId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static UserFavouritesResponseVM ToViewModel(MUserFavourites model)
        {
            if (model == null) return null;
            return new UserFavouritesResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                NutritionalItemId = model.NutritionalItemId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<UserFavouritesResponseVM> ToViewModelList(List<MUserFavourites> models)
        {
            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
