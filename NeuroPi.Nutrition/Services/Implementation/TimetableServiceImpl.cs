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
            // FIX: Force incoming date to UTC
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            var timetableData =
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
                      && t.Date.HasValue
                      && t.Date.Value.Date == date.Date
                      && r.ModuleId == moduleId   // ← added module filter
                select new
                {
                    t.Id,
                    t.Date,
                    t.Cycle,
                    t.Duration,
                    t.DailyWeekly,
                    Resource = r,
                    ResourceType = rt,
                    UserStatus = us,
                    StatusMaster = sm
                };

            var result = timetableData.ToList();

            var resourceIds = result
                .Where(x => x.Resource != null)
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

            return result.Select(x => new TimetableVM
            {
                Id = x.Id,
                Date = x.Date,

                // added here
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

                Instructions = (x.Resource != null
                                && instructionsLookup.ContainsKey(x.Resource.Id))
                                ? instructionsLookup[x.Resource.Id]
                                : new List<string>(),

                StatusId = x.UserStatus?.StatusId,
                StatusName = x.StatusMaster?.Name,
                StatusCode = x.StatusMaster?.Code

            }).ToList();
        }


    }
}
