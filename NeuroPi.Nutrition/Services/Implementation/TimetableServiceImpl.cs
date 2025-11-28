using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.TimeTable;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class TimetableServiceImpl : ITimetable
    {
        private readonly NeutritionDbContext _context;

        public TimetableServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public List<TimetableVM> GetTimetable(int userId, DateTime date, int tenantId, int moduleId)
        {
            bool getAll = date == DateTime.MinValue;
            bool getCurrentWeek = date == DateTime.MinValue.AddDays(1);

            var query =
                from t in _context.Timetables
                join r in _context.ResourceMasters on t.ResourceId equals r.Id
                join rt in _context.ResourceTypes on r.TypeId equals rt.Id

                join us in _context.UserGamesStatuses.Where(x => x.UserId == userId)
                    on t.Id equals us.NutTimetableId into usj
                from us in usj.DefaultIfEmpty()

                where t.TenantId == tenantId && r.ModuleId == moduleId
                select new
                {
                    Timetable = t,
                    Resource = r,
                    ResourceType = rt,
                    UserStatus = us
                };

            // -----------------------------
            // DATE FILTERING
            // -----------------------------
            if (getCurrentWeek)
            {
                var today = DateTime.UtcNow.Date;

                // Monday = start of week
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek + 1);
                var endOfWeek = startOfWeek.AddDays(6);

                query = query.Where(x =>
                    x.Timetable.Date >= startOfWeek &&
                    x.Timetable.Date <= endOfWeek);
            }
            else if (!getAll)
            {
                query = query.Where(x => x.Timetable.Date == date.Date);
            }

            var rawList = query.ToList();

            // -----------------------------
            // REMOVE DUPLICATES
            // -----------------------------
            var grouped = rawList
                .GroupBy(x => x.Timetable.Id)
                .Select(g =>
                    g.FirstOrDefault(x => x.UserStatus != null) ?? g.First())
                .ToList();

            // -----------------------------
            // FINAL RESPONSE
            // -----------------------------
            var result = grouped.Select(x => new TimetableVM
            {
                Id = x.Timetable.Id,
                Date = x.Timetable.Date,
                Cycle = x.Timetable.Cycle,
                Duration = x.Timetable.Duration,
                DailyWeekly = x.Timetable.DailyWeekly,

                ResourceId = x.Resource.Id,
                ResourceName = x.Resource.Name,
                ResourceShortName = x.Resource.ShortName,
                ResourceDescription = x.Resource.Description,
                PreviewUrl = x.Resource.PreviewUrl,
                Image = x.Resource.PreviewUrl,
                Script = x.Resource.Script,
                ResourceType = x.ResourceType.Name,

                Instructions = _context.ResourceInstructions
                    .Where(i => i.ResourceId == x.Resource.Id)
                    .OrderBy(i => i.Id)
                    .Select(i => i.Description)
                    .ToList(),

                StatusId = x.UserStatus?.StatusId ?? 0,
                StatusName = x.UserStatus?.Status?.Name ?? "Not Started",
                StatusCode = x.UserStatus?.Status?.Code ?? "NOT_STARTED"

            }).OrderBy(x => x.Date).ToList();

            return result;
        }
    }
}
