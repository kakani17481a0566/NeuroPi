using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserFavourites;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UserFavouritesServiceImpl : IUserFavourites
    {
        private readonly NeutritionDbContext _context;

        public UserFavouritesServiceImpl(NeutritionDbContext nutritionDbContext)
        {
            _context = nutritionDbContext;
        }

        public UserFavouritesResponseVM CreateUserFavourites(UserFavouritesRequestVM vm)
        {
            var fav = _context.UserFavourites
                .FirstOrDefault(x =>
                    x.UserId == vm.UserId &&
                    x.NutritionalItemId == vm.NutritionalItemId &&
                    x.TenantId == vm.TenantId);

            // CASE 1: Exists but soft-deleted → restore
            if (fav != null)
            {
                if (fav.IsDeleted)
                {
                    fav.IsDeleted = false;
                    fav.UpdatedOn = DateTime.UtcNow;
                    fav.UpdatedBy = vm.CreatedBy;

                    _context.SaveChanges();

                    var res1 = UserFavouritesResponseVM.ToViewModel(fav);
                    res1.Message = "Favourite restored successfully.";
                    return res1;
                }

                // CASE 2: Exists and active
                var res2 = UserFavouritesResponseVM.ToViewModel(fav);
                res2.Message = "Favourite already exists.";
                return res2;
            }

            // CASE 3: Create new favourite
            var model = UserFavouritesRequestVM.ToModel(vm);
            model.CreatedOn = DateTime.UtcNow;
            model.IsDeleted = false;

            _context.UserFavourites.Add(model);
            _context.SaveChanges();

            var res3 = UserFavouritesResponseVM.ToViewModel(model);
            res3.Message = "Favourite created successfully.";
            return res3;
        }

        public bool DeleteUserFavourites(int userId, int nutritionalItemId, int tenantId)
        {
            var favourite = _context.UserFavourites
         .FirstOrDefault(uf =>
             uf.UserId == userId &&
             uf.NutritionalItemId == nutritionalItemId &&
             uf.TenantId == tenantId);

            if (favourite == null)
            {
                return false;
            }

            // Hard delete
            _context.UserFavourites.Remove(favourite);
            _context.SaveChanges();

            return true;
        }

        public List<UserFavouritesResponseVM> GetUserFavourites()
        {
            var favourites = _context.UserFavourites
                .Where(uf => !uf.IsDeleted)
                .ToList();
            if (favourites == null || favourites.Count == 0)
            {
                return new List<UserFavouritesResponseVM>();
            }
            return UserFavouritesResponseVM.ToViewModelList(favourites);
        }

        public UserFavouritesResponseVM GetUserFavouritesById(int id)
        {
            var userFavourite = _context.UserFavourites
                .FirstOrDefault(uf => uf.Id == id && !uf.IsDeleted);
            if (userFavourite != null)
            {
                return UserFavouritesResponseVM.ToViewModel(userFavourite);
            }
            return null;
        }

        public UserFavouritesResponseVM GetUserFavouritesByIdAndTenantId(int id, int tenantId)
        {
            var userFavourite = _context.UserFavourites
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenantId && !uf.IsDeleted);
            if (userFavourite != null)
            {
                return UserFavouritesResponseVM.ToViewModel(userFavourite);
            }
            return null;
        }

        public List<UserFavouritesResponseVM> GetUserFavouritesByTenantId(int tenantId)
        {
            var UserFavourites = _context.UserFavourites
                .Where(uf => uf.TenantId == tenantId && !uf.IsDeleted)
                .ToList();
            if (UserFavourites == null || UserFavourites.Count == 0)
            {
                return new List<UserFavouritesResponseVM>();
            }
            return UserFavouritesResponseVM.ToViewModelList(UserFavourites);

        }



        public UserFavouritesResponseVM UpdateUserFavourites(int userId,int tenantId,int nutritionalItemId,
UserFavouritesUpdateVM vm)
        {
            // Check favourite by user + tenant + item
            var fav = _context.UserFavourites
                .FirstOrDefault(x =>
                    x.UserId == userId &&
                    x.TenantId == tenantId &&
                    x.NutritionalItemId == nutritionalItemId);

            if (fav == null)
            {
                return new UserFavouritesResponseVM
                {
                    Message = "This item is not present in your favourites."
                };
            }

            List<string> messages = new List<string>();

            // Toggle soft delete
            if (fav.IsDeleted)
            {
                fav.IsDeleted = false;
                messages.Add("Favourite restored.");
            }
            else
            {
                fav.IsDeleted = true;
                messages.Add("Favourite soft-deleted.");
            }

            // Update item (only nutritional item changes)
            if (fav.NutritionalItemId != vm.NutritionalItemId)
            {
                messages.Add(
                    $"NutritionalItemId changed from {fav.NutritionalItemId} to {vm.NutritionalItemId}."
                );

                fav.NutritionalItemId = vm.NutritionalItemId;
            }

            // UserId is not updated (as per your rule)

            fav.UpdatedBy = vm.UpdatedBy;
            fav.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            var result = UserFavouritesResponseVM.ToViewModel(fav);
            result.Message = string.Join(" ", messages);

            return result;
        }


    }
}
