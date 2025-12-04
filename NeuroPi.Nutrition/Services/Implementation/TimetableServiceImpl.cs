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
            // -----------------------------
            // DATE FILTER FLAGS
            // -----------------------------
            bool getAll = date == DateTime.MinValue;
            bool getCurrentWeek = date == DateTime.MinValue.AddDays(1);

            // -----------------------------
            // BASE QUERY – LOAD TIMETABLE + RESOURCE INFO
            // -----------------------------
            var timetableQuery =
                from t in _context.Timetables
                join r in _context.ResourceMasters on t.ResourceId equals r.Id into rj
                from r in rj.DefaultIfEmpty()

                join rt in _context.ResourceTypes on r.TypeId equals rt.Id into rtj
                from rt in rtj.DefaultIfEmpty()

                where t.TenantId == tenantId &&
                      r.ModuleId == moduleId
                select new
                {
                    Id = t.Id,
                    Date = t.Date,
                    Cycle = t.Cycle,
                    Duration = t.Duration,
                    DailyWeekly = t.DailyWeekly,
                    Resource = r,
                    ResourceType = rt
                };

            // -----------------------------
            // APPLY DATE FILTERS
            // -----------------------------
            if (getCurrentWeek)
            {
                DateTime today = DateTime.UtcNow.Date;
                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

                DateTime weekStart = today.AddDays(-diff);
                DateTime weekEnd = weekStart.AddDays(6);

                timetableQuery = timetableQuery.Where(x =>
                    x.Date.HasValue &&
                    x.Date.Value.Date >= weekStart &&
                    x.Date.Value.Date <= weekEnd);
            }
            else if (!getAll)
            {
                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                timetableQuery = timetableQuery.Where(x =>
                    x.Date.HasValue &&
                    x.Date.Value.Date == date.Date);
            }

            // LOAD RAW
            var timetableList = timetableQuery
                .AsEnumerable()
                .GroupBy(x => x.Id)       // remove duplicates
                .Select(g => g.First())
                .ToList();

            var timetableIds = timetableList.Select(x => x.Id).ToList();

            // -----------------------------
            // USER STATUSES (LATEST PER TIMETABLE)
            // -----------------------------
            var statuses = _context.UserGamesStatuses
                .Where(s =>
                    s.UserId == userId &&
                    s.NutTimetableId.HasValue &&
                    timetableIds.Contains(s.NutTimetableId.Value))
                .AsEnumerable()
                .GroupBy(s => s.NutTimetableId)
                .Select(g => g.OrderByDescending(x => x.UpdatedOn).First())
                .ToList();

            // Load Status Master
            var statusMasterIds = statuses
                .Where(s => s.StatusId.HasValue)
                .Select(s => s.StatusId.Value)
                .Distinct()
                .ToList();

            var statusMasters = _context.NutritionMasters
                .Where(m => statusMasterIds.Contains(m.Id))
                .ToList();

            // -----------------------------
            // LOAD RESOURCE INSTRUCTIONS
            // -----------------------------
            var resourceIds = timetableList
                .Where(x => x.Resource != null)
                .Select(x => x.Resource.Id)
                .Distinct()
                .ToList();

            Dictionary<int, List<string>> instructionsLookup = new();

            if (resourceIds.Any())
            {
                instructionsLookup = _context.ResourceInstructions
                    .Where(i => i.ResourceId.HasValue && resourceIds.Contains(i.ResourceId.Value))
                    .AsEnumerable()
                    .GroupBy(i => i.ResourceId.Value)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(i => i.Description).ToList()
                    );
            }

            // -----------------------------
            // FINAL MERGE (STATUS + INSTRUCTIONS)
            // -----------------------------
            var finalList = timetableList.Select(x =>
            {
                var status = statuses.FirstOrDefault(s => s.NutTimetableId == x.Id);
                var statusMaster = (status != null && status.StatusId.HasValue)
                    ? statusMasters.FirstOrDefault(m => m.Id == status.StatusId.Value)
                    : null;

                return new TimetableVM
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

                    StatusId = status?.StatusId,
                    StatusCode = statusMaster?.Code ?? "NOT_STARTED",
                    StatusName = statusMaster?.Name ?? "Not Started"
                };
            })
            .OrderBy(x => x.Date)
            .ToList();

            return finalList;
        }

    }
}
