using NeuroPi.Nutrition.ViewModel.UserGamesStatus;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUserGamesStatuses
    {
        /// <summary>
        /// Insert or update the user game status.
        /// </summary>
        /// <param name="model">Incoming request</param>
        /// <returns>UserGameStatusVM with full status details</returns>
        UserGameStatusVM SaveUserGameStatus(SaveUserGameStatusVM model);


        /// <summary>
        /// Get a user's timetable activity status.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="timetableId"></param>
        /// <param name="tenantId"></param>
        /// <returns>UserGameStatusVM (or null if not found)</returns>
        UserGameStatusVM? GetUserGameStatus(int userId, int timetableId, int tenantId);
    }
}
