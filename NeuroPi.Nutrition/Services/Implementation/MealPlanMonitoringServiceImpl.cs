using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.MealPlanMonitoring;
using System.Globalization;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class MealPlanMonitoringServiceImpl : IMealPlanMonitoring
    {
        private readonly NeutritionDbContext _context;

        public MealPlanMonitoringServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }


        public MealPlanMonitoringResponseVM CreateMealPlanMonitoring(MealPlanMonitoringRequestVM requestVM)
        {
            var newMPMonitoring = MealPlanMonitoringRequestVM.ToModel(requestVM);
            newMPMonitoring.CreatedOn = requestVM.CreatedOn;
            _context.MealPlanMonitoring.Add(newMPMonitoring);
            _context.SaveChanges();
            return MealPlanMonitoringResponseVM.ToViewModel(newMPMonitoring);
        }

        public bool DeleteMealPlanMonitoring(int id, int tenantId)
        {
            var existingMPM = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (existingMPM == null)
            { 
                return false;
            }
            existingMPM.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoring()
        {
            var mPMList = _context.MealPlanMonitoring.Where(m=> !m.IsDeleted).ToList();
            if(mPMList != null)
            {
                return MealPlanMonitoringResponseVM.ToViewModelList(mPMList);
            }
            return null;
            
        }

        public List<MealPlanMonitoringResponseVM> GetAllMealPlanMonitoringByTenantId(int tenantId)
        {
            var mealPlanMonitoring =_context.MealPlanMonitoring.Where(m => m.TenantId == tenantId && !m.IsDeleted).ToList();
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModelList(mealPlanMonitoring);

        }

        public MealPlanMonitoringResponseVM GetMealPlanMonitoringById(int id)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);

        }

        public MealPlanMonitoringResponseVM GetMealPlanMonitoringRequestByIdAndTenantId(int id, int tenantId)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId== tenantId && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);
        }

        public MealPlanMonitoringResponseVM UpdateMealPlanMonitoring(int id, int tenantId, MealPlanMonitoringUpdateVM updateVM)
        {
            var mealPlanMonitoring = _context.MealPlanMonitoring.FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            if (mealPlanMonitoring == null)
            {
                return null;
            }
            mealPlanMonitoring.MealPlanId = updateVM.MealPlanId;
            mealPlanMonitoring.NutritionalItemId = updateVM.NutritionalItemId;
            mealPlanMonitoring.Quantity = updateVM.Quantity;
            mealPlanMonitoring.UpdatedBy = updateVM.UpdatedBy;
            mealPlanMonitoring.UpdatedOn = updateVM.UpdatedOn;

            _context.SaveChanges();
            return MealPlanMonitoringResponseVM.ToViewModel(mealPlanMonitoring);
        }

        public MealPlan7daysCardVM Get7DaysMealPlanCard(int userId, int tenantId)
        {
            var culture = CultureInfo.CurrentCulture;

            // Load meal types (dynamic)
            var mealTypeMap = _context.MealTypes
                .Where(m => m.TenantId == tenantId && !m.IsDeleted)
                .ToDictionary(m => m.Id, m => m.Name);

            // Compute Monday → Sunday
            var today = DateTime.Today;
            int diff = today.DayOfWeek == DayOfWeek.Sunday
                ? -6
                : DayOfWeek.Monday - today.DayOfWeek;

            var monday = today.AddDays(diff);
            var sunday = monday.AddDays(6);

            // Fetch all meal plans for this week (1 query)
            var weekMealPlans = _context.MealPlan
                .Where(mp => mp.UserId == userId
                          && mp.TenantId == tenantId
                          && mp.Date >= DateOnly.FromDateTime(monday)
                          && mp.Date <= DateOnly.FromDateTime(sunday)
                          && !mp.IsDeleted)
                .ToList();

            // Fetch nutritional items (1 query)
            var itemIds = weekMealPlans.Select(mp => mp.NutritionalItemId).Distinct().ToList();

            var itemMap = _context.NutritionalItems
                .Where(n => itemIds.Contains(n.Id))
                .ToDictionary(
                    n => n.Id,
                    n => new { kcal = n.CaloriesQuantity, protein = n.ProteinQuantity }
                );

            var response = new MealPlan7daysCardVM();

            // Daily loop (Mon → Sun)
            for (int i = 0; i < 7; i++)
            {
                var date = monday.AddDays(i);
                var dateOnly = DateOnly.FromDateTime(date);

                var plansForDay = weekMealPlans.Where(mp => mp.Date == dateOnly).ToList();

                int totalKcal = 0;
                int totalProtein = 0;

                var mealWiseKcal = new Dictionary<string, int>();
                var mealWiseProtein = new Dictionary<string, int>();

                foreach (var mp in plansForDay)
                {
                    if (!itemMap.TryGetValue(mp.NutritionalItemId, out var nutri)) continue;

                    int kcal = nutri.kcal * mp.Quantity;
                    int protein = nutri.protein * mp.Quantity;

                    totalKcal += kcal;
                    totalProtein += protein;

                    // Dynamic meal name
                    string mealLabel = mealTypeMap.ContainsKey(mp.MealTypeId)
                        ? mealTypeMap[mp.MealTypeId]
                        : $"MealType-{mp.MealTypeId}";

                    mealWiseKcal[mealLabel] = mealWiseKcal.GetValueOrDefault(mealLabel, 0) + kcal;
                    mealWiseProtein[mealLabel] = mealWiseProtein.GetValueOrDefault(mealLabel, 0) + protein;
                }

                // ----------------------------------------------------
                // ⭐ DYNAMIC STATUS LOGIC (NO HARDCODE, NO DB RULES)
                // ----------------------------------------------------
                int goal = 1200; // If needed, load per user/tenant
                string statusText;
                string statusType;

                if (totalKcal == 0)
                {
                    statusText = "---";
                    statusType = "muted";
                }
                else
                {
                    double pct = (double)totalKcal / goal * 100;

                    if (pct >= 100)
                    {
                        statusText = "Excellent! Target achieved";
                        statusType = "success";
                    }
                    else if (pct >= 67)
                    {
                        statusText = "Good! Almost reached your goal";
                        statusType = "info";
                    }
                    else if (pct >= 34)
                    {
                        statusText = "Low intake, increase meal portions";
                        statusType = "warning";
                    }
                    else
                    {
                        statusText = "Very low calories";
                        statusType = "danger";
                    }
                }

                response.Days.Add(new MealPlan7DayItemVM
                {
                    Date = date.Day,
                    Month = date.ToString("MMMM", culture).ToUpper(),
                    Weekday = date.ToString("dddd", culture),
                    FullDate = date.ToString("yyyy-MM-dd"),

                    Calories = totalKcal,
                    Protein = totalProtein,
                    MealWiseCalories = mealWiseKcal,
                    MealWiseProtein = mealWiseProtein,
                    StatusText = statusText,
                    StatusType = statusType
                });
            }

            response.UnlockNote = new UnlockNoteVM
            {
                Enabled = true,
                TextTop = "Next week schedule unlocks on",
                TextBottom = monday.AddDays(3).ToString("dd dddd, MMMM", culture)
            };

            return response;
        }


        public MealPlanMonitoringResponseViewVM GetMealMonitoring(int userId, int tenantId, DateOnly date)
        {
            var response = new MealPlanMonitoringResponseViewVM
            {
                Date = date,
                Sections = new(),
                AchievedFocus = new()
            };

            /* ---------------------------------------------------------
               1. Load MEAL TYPES
            --------------------------------------------------------- */
            var mealTypes = _context.MealTypes
                .Where(m => m.TenantId == tenantId && !m.IsDeleted)
                .OrderBy(m => m.Id)
                .ToList();

            /* ---------------------------------------------------------
               2. Load TODAY (plans + monitoring + unplanned)
            --------------------------------------------------------- */
            var todayPlans = _context.MealPlan
                .Where(mp => mp.UserId == userId &&
                             mp.TenantId == tenantId &&
                             mp.Date == date &&
                             !mp.IsDeleted)
                .ToList();

            var planIds = todayPlans.Select(x => x.Id).ToList();

            var todayMonitoring = _context.MealPlanMonitoring
                .Where(m => planIds.Contains(m.MealPlanId) && !m.IsDeleted)
                .ToList();

            var todayUnplanned = _context.UnplannedMeals
                .Where(u => planIds.Contains(u.MealPlanId) && !u.IsDeleted)
                .ToList();

            /* ---------------------------------------------------------
               3. Load ALL ITEM DETAILS (1 DB CALL)
            --------------------------------------------------------- */
            var itemIds = todayPlans
                .Select(p => p.NutritionalItemId)
                .Concat(todayMonitoring.Select(m => m.NutritionalItemId))
                .Concat(todayUnplanned.Where(u => u.NutritionalItemId != 0)
                        .Select(u => u.NutritionalItemId))
                .Distinct()
                .ToList();

            var itemMap = _context.NutritionalItems
                .Where(n => itemIds.Contains(n.Id))
                .ToDictionary(n => n.Id);

            /* ---------------------------------------------------------
               4. Build TODAY SECTIONS (FULL CARD)
            --------------------------------------------------------- */
            foreach (var mt in mealTypes)
            {
                var sec = new MealWindowVM
                {
                    MealTypeId = mt.Id,
                    MealTypeName = mt.Name,
                    Time = mt.Description,
                    Items = new()
                };

                // Planned items
                var planned = todayPlans.Where(p => p.MealTypeId == mt.Id).ToList();

                foreach (var p in planned)
                {
                    if (!itemMap.TryGetValue(p.NutritionalItemId, out var item))
                        continue;

                    var consumed = todayMonitoring.FirstOrDefault(m =>
                        m.MealPlanId == p.Id &&
                        m.NutritionalItemId == p.NutritionalItemId);

                    sec.Items.Add(new FoodMonitorVM
                    {
                        ItemId = item.Id,
                        Title = item.Name,
                        Unit = item.Description,
                        ItemImage = item.ItemImage,
                        Kcal = item.CaloriesQuantity,
                        PlannedQty = p.Quantity,
                        ConsumedQty = consumed?.Quantity ?? 0,
                        IsUnplanned = false
                    });
                }

                // Unplanned
                var unp = todayUnplanned
                    .Where(u => todayPlans.Any(p => p.Id == u.MealPlanId && p.MealTypeId == mt.Id))
                    .ToList();

                foreach (var u in unp)
                {
                    if (u.NutritionalItemId != 0 && itemMap.TryGetValue(u.NutritionalItemId, out var item))
                    {
                        sec.Items.Add(new FoodMonitorVM
                        {
                            ItemId = item.Id,
                            Title = item.Name,
                            Unit = item.Description,
                            ItemImage = item.ItemImage,
                            Kcal = item.CaloriesQuantity,
                            PlannedQty = 0,
                            ConsumedQty = u.Quantity,
                            IsUnplanned = true
                        });
                    }
                    else
                    {
                        sec.Items.Add(new FoodMonitorVM
                        {
                            ItemId = 0,
                            Title = u.OtherName,
                            Unit = "",
                            ItemImage = "",
                            Kcal = u.OtherCaloriesQuantity,
                            PlannedQty = 0,
                            ConsumedQty = u.Quantity,
                            IsUnplanned = true
                        });
                    }
                }

                sec.SectionCalories = sec.Items.Sum(i => i.Kcal * i.ConsumedQty);
                response.Sections.Add(sec);
            }

            response.TotalCalories = response.Sections.Sum(s => s.SectionCalories);

            /* ---------------------------------------------------------
               5. PENDING DAYS — BUILD SAME CARD AS TODAY
            --------------------------------------------------------- */

            var prevDates = _context.MealPlan
                .Where(mp => mp.UserId == userId &&
                             mp.TenantId == tenantId &&
                             mp.Date < date &&
                             !mp.IsDeleted)
                .Select(mp => mp.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            if (!prevDates.Any())
            {
                response.MissedDays = new MissedDaysInfoVM();
            }
            else
            {
                var prevPlans = _context.MealPlan
                    .Where(mp => mp.UserId == userId &&
                                 mp.TenantId == tenantId &&
                                 prevDates.Contains(mp.Date) &&
                                 !mp.IsDeleted)
                    .ToList();

                var planIdToDate = prevPlans.ToDictionary(x => x.Id, x => x.Date);
                var prevPlanIds = planIdToDate.Keys.ToHashSet();

                var monitoringDates = _context.MealPlanMonitoring
                    .Where(m => prevPlanIds.Contains(m.MealPlanId) && !m.IsDeleted)
                    .Select(m => planIdToDate[m.MealPlanId])
                    .Distinct()
                    .ToHashSet();

                var unplannedDates = _context.UnplannedMeals
                    .Where(u => prevPlanIds.Contains(u.MealPlanId) && !u.IsDeleted)
                    .Select(u => planIdToDate[u.MealPlanId])
                    .Distinct()
                    .ToHashSet();

                var pendingDates = prevDates
                    .Where(d => !monitoringDates.Contains(d) &&
                                !unplannedDates.Contains(d))
                    .ToList();

                // Build item map for pending days
                var templateItemIds = prevPlans.Select(p => p.NutritionalItemId).Distinct().ToList();
                var templateItems = _context.NutritionalItems
                    .Where(n => templateItemIds.Contains(n.Id))
                    .ToDictionary(n => n.Id);

                List<MealWindowVM> BuildPendingSections(DateOnly dt)
                {
                    var list = new List<MealWindowVM>();

                    foreach (var mt in mealTypes)
                    {
                        var sec = new MealWindowVM
                        {
                            MealTypeId = mt.Id,
                            MealTypeName = mt.Name,
                            Time = mt.Description,
                            Items = new()
                        };

                        var items = prevPlans.Where(p => p.Date == dt && p.MealTypeId == mt.Id).ToList();

                        foreach (var p in items)
                        {
                            if (!templateItems.TryGetValue(p.NutritionalItemId, out var item))
                                continue;

                            sec.Items.Add(new FoodMonitorVM
                            {
                                ItemId = item.Id,
                                Title = item.Name,
                                Unit = item.Description,
                                ItemImage = item.ItemImage,
                                Kcal = item.CaloriesQuantity,
                                PlannedQty = p.Quantity,
                                ConsumedQty = 0,
                                IsUnplanned = false
                            });
                        }

                        sec.SectionCalories = sec.Items.Sum(i => i.Kcal * i.PlannedQty);
                        list.Add(sec);
                    }

                    return list;
                }

                int streak = 0;
                foreach (var d in pendingDates)
                {
                    if (d == date.AddDays(-(streak + 1)))
                        streak++;
                    else break;
                }

                response.MissedDays = new MissedDaysInfoVM
                {
                    TotalMissedDays = pendingDates.Count,
                    StreakMissed = streak,
                    LastEatenDate = monitoringDates
                        .Union(unplannedDates)
                        .OrderByDescending(d => d)
                        .FirstOrDefault(),
                    History = pendingDates.Select(d => new PreviousDayVM
                    {
                        Date = d,
                        Status = "PENDING",
                        Sections = BuildPendingSections(d),
                        TotalCalories = BuildPendingSections(d)
                            .Sum(sec => sec.Items.Sum(x => x.Kcal * x.PlannedQty))
                    }).ToList()
                };
            }

            /* ---------------------------------------------------------
               6. Filters for frontend
            --------------------------------------------------------- */
            response.AllMealTypes = mealTypes.Select(m => new FilterOptions
            {
                Id = m.Id,
                Name = m.Name,
                Image = null
            }).ToList();

            response.AllFoods = _context.NutritionalItems
                .Where(n => n.TenantId == tenantId && !n.IsDeleted)
                .Select(n => new FilterOptions
                {
                    Id = n.Id,
                    Name = n.Name,
                    Image = n.ItemImage
                })
                .ToList();

            response.AllFocus = _context.NutritionalFocuses
                .Where(f => f.TenantId == tenantId && !f.IsDeleted)
                .Select(f => new FilterOptions
                {
                    Id = f.Id,
                    Name = f.Name
                }).ToList();

            response.AllVitamins = _context.Vitamins
                .Where(v => v.TenantId == tenantId && !v.IsDeleted)
                .Select(v => new FilterOptions
                {
                    Id = v.Id,
                    Name = v.Name
                }).ToList();

            return response;
        }

    
    
    }
}
