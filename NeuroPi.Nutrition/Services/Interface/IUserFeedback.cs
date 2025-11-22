using NeuroPi.Nutrition.ViewModel;
using NeuroPi.Nutrition.ViewModel.UserFeedback;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUserFeedback
    {
        List<UserFeedbackResponseVM> GetAllUserFeedbacks();

        UserFeedbackResponseVM GetUserFeedbackById(int id);

        UserFeedbackResponseVM GetUserFeedbackByIdAndTenantId(int id, int tenantId);

        List<UserFeedbackResponseVM> GetUserFeedbacksByTenantId(int tenantId);

        UserFeedbackResponseVM CreateUserFeedback(UserFeedbackRequestVM RequestVM);

        UserFeedbackResponseVM UpdateUserFeedback(int id,int tenantId, UserFeedbackUpdateVM RequestVM);

        bool DeleteUserFeedback(int id, int tenantId);

        List<FeedbackQuestionVM> GetFeedbackQuestions(int userId, int tenantId);

        bool SaveUserFeedback(int userId, int tenantId, SaveFeedbackVM model);



    }

}