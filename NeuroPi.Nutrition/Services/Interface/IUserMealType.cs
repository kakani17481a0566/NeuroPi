using NeuroPi.Nutrition.ViewModel.UserMealType;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUserMealType
    {
        List<UserMealTypeResponseVM> GetAllUserMealTypes();

        UserMealTypeResponseVM GetUserMealTypeById(int id);

        UserMealTypeResponseVM GetUserMealTypeByIdAndTenantId(int id, int tenantId);

        List<UserMealTypeResponseVM> GetUserMealTypesByTenantId(int tenantId);

        UserMealTypeResponseVM CreateUserMealType(UserMealTypeRequestVM RequestVM);

        UserMealTypeResponseVM UpdateUserMealType(int id,int tenantId, UserMealTypeUpdateVM RequestVM);

        bool DeleteUserMealType(int id, int tenantId);
    }

}