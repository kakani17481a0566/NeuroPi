using NeuroPi.Nutrition.ViewModel.UserFavourites;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUserFavourites
    {
        List<UserFavouritesResponseVM> GetUserFavourites();

        UserFavouritesResponseVM GetUserFavouritesById(int id);

        List<UserFavouritesResponseVM> GetUserFavouritesByTenantId(int tenantId);

        UserFavouritesResponseVM GetUserFavouritesByIdAndTenantId(int id,int tenantId);

        UserFavouritesResponseVM CreateUserFavourites(UserFavouritesRequestVM userFavouritesRequestVM);
        UserFavouritesResponseVM UpdateUserFavourites(int id,int tenatId, UserFavouritesUpdateVM userFavouritesRequestVM);

        bool DeleteUserFavourites(int id,int tenantId);


    }
}
