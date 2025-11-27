using NeuroPi.Nutrition.ViewModel.TimeTable;
using NeuroPi.Nutrition.ViewModel.Vitamins;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface ITimetable
    {

        List<TimetableVM> GetTimetable(int userId, DateTime date, int tenantId, int moduleId);




    }
}
