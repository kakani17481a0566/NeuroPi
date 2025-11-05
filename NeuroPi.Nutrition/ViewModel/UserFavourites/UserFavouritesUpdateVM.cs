namespace NeuroPi.Nutrition.ViewModel.UserFavourites
{
    public class UserFavouritesUpdateVM
    {
        public int UserId { get; set; }
        public int NutritionalItemId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
