using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserMealType;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class UserMealTypeServiceImpl : IUserMealType
    {
        private readonly NeutritionDbContext _dbContext;

        public UserMealTypeServiceImpl(NeutritionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserMealTypeResponseVM CreateUserMealType(UserMealTypeRequestVM RequestVM)
        {
            var newUserMealType = UserMealTypeRequestVM.ToModel(RequestVM);
            _dbContext.UserMealTypes.Add(newUserMealType);
            _dbContext.SaveChanges();
            return UserMealTypeResponseVM.ToViewModel(newUserMealType);
        }

        public bool DeleteUserMealType(int id, int tenantId)
        {
            var userMealType = _dbContext.UserMealTypes
                .FirstOrDefault(umt => umt.Id == id && umt.TenantId == tenantId);
            if (userMealType == null)
            {
                return false;
            }
            userMealType.IsDeleted = true;
            _dbContext.SaveChanges();
            return true;
        }

        public List<UserMealTypeResponseVM> GetAllUserMealTypes()
        {
            var userMealTypes = _dbContext.UserMealTypes
                .Where(umt => !umt.IsDeleted)
                .ToList();
            if(userMealTypes == null)
            {
                return null;
            }
            return UserMealTypeResponseVM.ToViewModelList(userMealTypes);
        }

        public UserMealTypeResponseVM GetUserMealTypeById(int id)
        {
            var userMealType = _dbContext.UserMealTypes
                .FirstOrDefault(umt => umt.Id == id && !umt.IsDeleted);
            if (userMealType == null)
            {
                return null;
            }
            return UserMealTypeResponseVM.ToViewModel(userMealType);
        }

        public UserMealTypeResponseVM GetUserMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var userMealType = _dbContext.UserMealTypes
                .FirstOrDefault(umt => umt.Id == id && umt.TenantId == tenantId && !umt.IsDeleted);
            if (userMealType == null)
            {
                return null;
            }
                return UserMealTypeResponseVM.ToViewModel(userMealType);
            }

        public List<UserMealTypeResponseVM> GetUserMealTypesByTenantId(int tenantId)
        {
            var userMealTypes = _dbContext.UserMealTypes
                .Where(umt => umt.TenantId == tenantId && !umt.IsDeleted)
                .ToList();
            if (userMealTypes == null)
            {
                return null;
            }
            return UserMealTypeResponseVM.ToViewModelList(userMealTypes);
        }

        public UserMealTypeResponseVM UpdateUserMealType(int id, int tenantId, UserMealTypeUpdateVM RequestVM)
        {
            var userMealType = _dbContext.UserMealTypes
                .FirstOrDefault(umt => umt.Id == id && umt.TenantId == tenantId && !umt.IsDeleted);
            if (userMealType == null)
            {
                return null;
            }
            userMealType.UserId = RequestVM.UserId;
            userMealType.MealTypeId = RequestVM.MealTypeId;
            userMealType.PreferredMealTime = RequestVM.PreferredMealTime;
            userMealType.UpdatedBy = RequestVM.UpdatedBy;
            userMealType.UpdatedOn = RequestVM.UpdatedOn;
            _dbContext.SaveChanges();
            return UserMealTypeResponseVM.ToViewModel(userMealType);

        }
    }
}
