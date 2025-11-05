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

        public UserFavouritesResponseVM CreateUserFavourites(UserFavouritesRequestVM userFavouritesRequestVM)
        {
            var newUserFavourite = UserFavouritesRequestVM.ToModel(userFavouritesRequestVM);
            newUserFavourite.CreatedOn = DateTime.UtcNow;
            _context.UserFavourites.Add(newUserFavourite);
            _context.SaveChanges();
            return UserFavouritesResponseVM.ToViewModel(newUserFavourite);

        }

        public bool DeleteUserFavourites(int id, int tenantId)
        {
            var existingFavourite = _context.UserFavourites
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenantId&&!uf.IsDeleted);
            if (existingFavourite == null)
            {
                return false;
            }
            existingFavourite.IsDeleted = true;
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

        public UserFavouritesResponseVM UpdateUserFavourites(int id, int tenatId, UserFavouritesUpdateVM userFavouritesRequestVM)
        {
            var existingFavourite = _context.UserFavourites
                .FirstOrDefault(uf => uf.Id == id && uf.TenantId == tenatId && !uf.IsDeleted);
            if (existingFavourite == null)
            {
                return null;
            }
            
            existingFavourite.UserId = userFavouritesRequestVM.UserId;
            existingFavourite.NutritionalItemId = userFavouritesRequestVM.NutritionalItemId;
            existingFavourite.UpdatedBy = userFavouritesRequestVM.UpdatedBy;
            existingFavourite.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return UserFavouritesResponseVM.ToViewModel(existingFavourite); 
        }
    }
}
