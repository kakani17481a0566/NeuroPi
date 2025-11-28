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
            // Handle special flags
            bool getAll = date == DateTime.MinValue;
            bool getCurrentWeek = date == DateTime.MinValue.AddDays(1);

            // Base Query
            var baseQuery =
                from t in _context.Timetables
                join r in _context.ResourceMasters
                    on t.ResourceId equals r.Id into rj
                from r in rj.DefaultIfEmpty()

                join rt in _context.ResourceTypes
                    on r.TypeId equals rt.Id into rtj
                from rt in rtj.DefaultIfEmpty()

                join us in _context.UserGamesStatuses
                    on new { Tid = (int?)t.Id, Uid = (int?)userId }
                    equals new { Tid = us.NutTimetableId, Uid = us.UserId } into usj
                from us in usj.DefaultIfEmpty()

                join sm in _context.NutritionMasters
                    on us.StatusId equals sm.Id into smj
                from sm in smj.DefaultIfEmpty()

                where t.TenantId == tenantId
                      && r.ModuleId == moduleId
                select new
                {
                    t.Id,
                    t.Date,
                    t.Cycle,
                    t.Duration,
                    t.DailyWeekly,
                    Resource = r,
                    ResourceType = rt,

                    // ⭐ USER STATUS HANDLING
                    StatusId = us != null ? us.StatusId : (int?)null,

                    StatusCode = sm != null ? sm.Code : "NOT_STARTED",
                    StatusName = sm != null ? sm.Name : "Not Started"
                };

            // Apply date filters
            if (!getAll && !getCurrentWeek)
            {
                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                baseQuery = baseQuery.Where(x => x.Date.HasValue && x.Date.Value.Date == date.Date);
            }
            else if (getCurrentWeek)
            {
                DateTime today = DateTime.UtcNow.Date;
                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                DateTime weekStart = today.AddDays(-diff);
                DateTime weekEnd = weekStart.AddDays(6);

                baseQuery = baseQuery.Where(x =>
                    x.Date.HasValue &&
                    x.Date.Value.Date >= weekStart &&
                    x.Date.Value.Date <= weekEnd
                );
            }

            var result = baseQuery.ToList();

            // Instructions Lookup
            var resourceIds = result.Where(x => x.Resource != null)
                                    .Select(x => x.Resource.Id)
                                    .Distinct()
                                    .ToList();

            var instructionsLookup = new Dictionary<int, List<string>>();

            if (resourceIds.Count > 0)
            {
                instructionsLookup = _context.ResourceInstructions
                    .Where(i => resourceIds.Contains(i.ResourceId.Value))
                    .OrderBy(i => i.Id)
                    .ToList()
                    .GroupBy(i => i.ResourceId.Value)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(i => i.Description).ToList()
                    );
            }

            // Final Map
            return result.Select(x => new TimetableVM
            {
                Id = x.Id,
                Date = x.Date,
                Cycle = x.Cycle,
                Duration = x.Duration,
                DailyWeekly = x.DailyWeekly,

                ResourceId = x.Resource?.Id,
                ResourceName = x.Resource?.Name,
                ResourceShortName = x.Resource?.ShortName,
                ResourceDescription = x.Resource?.Description,
                PreviewUrl = x.Resource?.PreviewUrl,
                Image = x.Resource?.Image,
                Script = x.Resource?.Script,
                ResourceType = x.ResourceType?.Name,

                Instructions = (x.Resource != null &&
                                instructionsLookup.ContainsKey(x.Resource.Id))
                                ? instructionsLookup[x.Resource.Id]
                                : new List<string>(),

                StatusId = x.StatusId,
                StatusName = x.StatusName,
                StatusCode = x.StatusCode

            }).ToList();
        }


    }
}
