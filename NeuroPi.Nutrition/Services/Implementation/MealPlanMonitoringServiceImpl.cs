using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
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

            // Load meal types
            var mealTypeMap = _context.MealTypes
                .Where(m => m.TenantId == tenantId && !m.IsDeleted)
                .ToDictionary(m => m.Id, m => m.Name);

            // Compute Monday → Sunday of THIS week
            var today = DateTime.Today;

            int diff = today.DayOfWeek == DayOfWeek.Sunday
                ? -6
                : DayOfWeek.Monday - today.DayOfWeek;

            var monday = today.AddDays(diff);
            var sunday = monday.AddDays(6);

            // Fetch plans for the week
            var weekMealPlans = _context.MealPlan
                .Where(mp => mp.UserId == userId
                          && mp.TenantId == tenantId
                          && mp.Date >= DateOnly.FromDateTime(monday)
                          && mp.Date <= DateOnly.FromDateTime(sunday)
                          && !mp.IsDeleted)
                .ToList();

            // Load nutritional info
            var itemIds = weekMealPlans.Select(mp => mp.NutritionalItemId).Distinct().ToList();

            var itemMap = _context.NutritionalItems
                .Where(n => itemIds.Contains(n.Id))
                .ToDictionary(
                    n => n.Id,
                    n => new { kcal = n.CaloriesQuantity, protein = n.ProteinQuantity }
                );

            var response = new MealPlan7daysCardVM();

            // Loop for each day of the week (Mon → Sun)
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

                    string mealLabel = mealTypeMap.ContainsKey(mp.MealTypeId)
                        ? mealTypeMap[mp.MealTypeId]
                        : $"MealType-{mp.MealTypeId}";

                    mealWiseKcal[mealLabel] = mealWiseKcal.GetValueOrDefault(mealLabel, 0) + kcal;
                    mealWiseProtein[mealLabel] = mealWiseProtein.GetValueOrDefault(mealLabel, 0) + protein;
                }


                // ------------------------------
                // ⭐ STATUS LOGIC
                // ------------------------------
                int goal = 1200; // can be dynamic later

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

                // Add day summary to response
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

            // ----------------------------------------------------
            // ⭐ NEXT MONDAY UNLOCK LOGIC (Works for February too)
            // ----------------------------------------------------
            var todayLocal = today;

            // Calculate next Monday
            var nextMonday = todayLocal.AddDays(
                ((int)DayOfWeek.Monday - (int)todayLocal.DayOfWeek + 7) % 7
            );

            // If today is Monday, unlock next week (after 7 days)
            if (nextMonday == todayLocal)
                nextMonday = todayLocal.AddDays(7);

            response.UnlockNote = new UnlockNoteVM
            {
                Enabled = true,
                TextTop = "Next week schedule unlocks on",
                TextBottom = nextMonday.ToString("dd dddd, MMMM", culture)
            };

            return response;
        }









        public MealPlanMonitoringResponseViewVM GetMealMonitoring(int userId, int tenantId, DateOnly date)
        {
            var response = new MealPlanMonitoringResponseViewVM
            {
                Date = date,
                Sections = new(),
                AchievedFocus = new(),
                MissedDays = new MissedDaysInfoVM()
            };

            /* ============================================================
               1. LOAD MEAL TYPES
            ============================================================ */
            var mealTypes = _context.MealTypes
                .Where(m => m.TenantId == tenantId && !m.IsDeleted)
                .OrderBy(m => m.Id)
                .ToList();

            /* ============================================================
               2. LOAD TODAY’S MEAL PLAN, MONITORING, UNPLANNED
            ============================================================ */
            var todayPlans = _context.MealPlan
                .Where(mp =>
                    mp.UserId == userId &&
                    mp.TenantId == tenantId &&
                    mp.Date == date &&
                    !mp.IsDeleted)
                .ToList();

            var todayPlanIds = todayPlans.Select(p => p.Id).ToList();

            var todayMonitoring = _context.MealPlanMonitoring
                .Where(m => todayPlanIds.Contains(m.MealPlanId) && !m.IsDeleted)
                .ToList();

            var todayUnplanned = _context.UnplannedMeals
                .Where(u => todayPlanIds.Contains(u.MealPlanId) && !u.IsDeleted)
                .ToList();

            /* ============================================================
               3. LOAD ALL ITEMS USED TODAY
            ============================================================ */
            var todayItemIds = todayPlans.Select(p => p.NutritionalItemId)
                .Concat(todayMonitoring.Select(m => m.NutritionalItemId))
                .Concat(todayUnplanned.Where(u => u.NutritionalItemId != 0).Select(u => u.NutritionalItemId))
                .Distinct()
                .ToList();

            var itemMap = _context.NutritionalItems
                .Where(n => todayItemIds.Contains(n.Id) && !n.IsDeleted)
                .ToDictionary(n => n.Id);

            /* ============================================================
               4. BUILD TODAY’S SECTIONS
            ============================================================ */
            foreach (var mt in mealTypes)
            {
                var sec = new MealWindowVM
                {
                    MealTypeId = mt.Id,
                    MealTypeName = mt.Name,
                    Time = mt.Description,
                    Items = new()
                };

                // ----- PLANNED ITEMS -----
                var planned = todayPlans.Where(p => p.MealTypeId == mt.Id).ToList();

                foreach (var p in planned)
                {
                    if (!itemMap.TryGetValue(p.NutritionalItemId, out var item))
                        continue;

                    var consumed = todayMonitoring
                        .Where(m => m.MealPlanId == p.Id && m.NutritionalItemId == p.NutritionalItemId)
                        .Sum(m => m.Quantity);

                    sec.Items.Add(new FoodMonitorVM
                    {
                        ItemId = item.Id,
                        Title = item.Name,
                        Unit = item.Description,
                        ItemImage = item.ItemImage,
                        Kcal = item.CaloriesQuantity,
                        PlannedQty = p.Quantity,
                        ConsumedQty = consumed,
                        IsUnplanned = false
                    });
                }

                // ----- UNPLANNED ITEMS -----
                var unplannedItems = todayUnplanned
                    .Where(u => planned.Any(p => p.Id == u.MealPlanId))
                    .ToList();

                foreach (var u in unplannedItems)
                {
                    if (u.NutritionalItemId != 0 &&
                        itemMap.TryGetValue(u.NutritionalItemId, out var item))
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

            /* ============================================================
               5. ACHIEVED FOCUS FOR TODAY
            ============================================================ */
            var focusItemMap = _context.NutritionalFocusItem
                .Where(f => f.TenantId == tenantId && !f.IsDeleted)
                .Include(f => f.NutritionalFocus)
                .Include(f => f.NutritionalItem)
                .ToList();

            var todayConsumedIds = response.Sections
                .SelectMany(s => s.Items)
                .Where(i => i.ConsumedQty > 0)
                .Select(i => i.ItemId)
                .Distinct()
                .ToList();

            var achievedFocusIds = focusItemMap
                .Where(f => todayConsumedIds.Contains(f.NutritionalItemId))
                .Select(f => f.NutritionalFocusId)
                .Distinct()
                .ToList();

            response.AchievedFocus = _context.NutritionalFocuses
                .Where(f => achievedFocusIds.Contains(f.Id) && !f.IsDeleted)
                .Select(f => new FocusOptionVM { Id = f.Id, Label = f.Name })
                .ToList();


            /* ============================================================
               6. FIND ALL PREVIOUS DATES — EXCLUDE TODAY
            ============================================================ */
            var prevDates = _context.MealPlan
                .Where(mp =>
                    mp.UserId == userId &&
                    mp.TenantId == tenantId &&
                    mp.Date < date &&
                    !mp.IsDeleted)
                .Select(mp => mp.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            if (!prevDates.Any()) goto MASTER_FILTERS;


            /* ============================================================
               7. LOAD ALL PREVIOUS PLANS + TRACKING
            ============================================================ */
            var prevPlans = _context.MealPlan
                .Where(mp =>
                    mp.UserId == userId &&
                    mp.TenantId == tenantId &&
                    prevDates.Contains(mp.Date) &&
                    !mp.IsDeleted)
                .ToList();

            var prevPlanIds = prevPlans.Select(p => p.Id).ToList();

            var prevMonitoring = _context.MealPlanMonitoring
                .Where(m => prevPlanIds.Contains(m.MealPlanId) && !m.IsDeleted)
                .ToList();

            var prevUnplanned = _context.UnplannedMeals
                .Where(u => prevPlanIds.Contains(u.MealPlanId) && !u.IsDeleted)
                .ToList();


            /* ============================================================
               8. RULE A — STRICT PENDING LOGIC
            ============================================================ */

            var pendingDates = new List<DateOnly>();

            foreach (var d in prevDates)
            {
                var dayPlans = prevPlans.Where(p => p.Date == d).ToList();
                var dayPlanIds = dayPlans.Select(p => p.Id).ToList();

                // RULE A.1 → If any monitoring exists → NOT pending
                if (prevMonitoring.Any(m => dayPlanIds.Contains(m.MealPlanId)))
                    continue;

                // RULE A.2 → If any unplanned exists → NOT pending
                if (prevUnplanned.Any(u => dayPlanIds.Contains(u.MealPlanId)))
                    continue;

                // RULE A.3 → If planQty > 0 and NO tracking → pending
                bool isPending = dayPlans.Any(p => p.Quantity > 0);

                if (isPending)
                    pendingDates.Add(d);
            }

            if (!pendingDates.Any()) goto MASTER_FILTERS;


            /* ============================================================
               9. BUILD PENDING HISTORY
            ============================================================ */
            var pendingHistory = new List<PreviousDayVM>();

            foreach (var pd in pendingDates)
            {
                var dayPlans = prevPlans.Where(p => p.Date == pd).ToList();

                var itemIds = dayPlans.Select(p => p.NutritionalItemId).Distinct().ToList();

                var pendItemMap = _context.NutritionalItems
                    .Where(n => itemIds.Contains(n.Id) && !n.IsDeleted)
                    .ToDictionary(n => n.Id);

                var sections = new List<MealWindowVM>();

                foreach (var mt in mealTypes)
                {
                    var sec = new MealWindowVM
                    {
                        MealTypeId = mt.Id,
                        MealTypeName = mt.Name,
                        Time = mt.Description,
                        Items = new()
                    };

                    var items = dayPlans.Where(p => p.MealTypeId == mt.Id).ToList();

                    foreach (var p in items)
                    {
                        if (!pendItemMap.TryGetValue(p.NutritionalItemId, out var item))
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

                    sections.Add(sec);
                }

                pendingHistory.Add(new PreviousDayVM
                {
                    Date = pd,
                    Status = "PENDING",
                    Sections = sections,
                    TotalCalories = sections.Sum(s => s.Items.Sum(i => i.Kcal * i.PlannedQty))
                });
            }

            response.MissedDays = new MissedDaysInfoVM
            {
                TotalMissedDays = pendingDates.Count,
                StreakMissed = pendingDates.Count,
                LastEatenDate = prevDates.Where(d => d < pendingDates.Last()).FirstOrDefault(),
                History = pendingHistory
            };


        MASTER_FILTERS:

            /* ============================================================
               10. MASTER FILTER OPTIONS
            ============================================================ */
            response.AllMealTypes = mealTypes
                .Select(m => new MealTypeOptionVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Time = m.Description
                })
                .ToList();

            response.AllFoods = _context.NutritionalItems
                .Where(n => n.TenantId == tenantId && !n.IsDeleted)
                .Select(n => new FoodOptionVM
                {
                    Id = n.Id,
                    Name = n.Name,
                    Image = n.ItemImage,
                    Kcal = n.CaloriesQuantity,
                    Unit = n.Description
                })
                .ToList();

            response.AllFocus = _context.NutritionalFocuses
                .Where(f => f.TenantId == tenantId && !f.IsDeleted)
                .Select(f => new FocusOptionVM { Id = f.Id, Label = f.Name })
                .ToList();

            response.AllVitamins = _context.Vitamins
                .Where(v => v.TenantId == tenantId && !v.IsDeleted)
                .Select(v => new VitaminOptionVM { Id = v.Id, Name = v.Name })
                .ToList();

            response.AllFocusItems = focusItemMap
                .Select(f => new FocusItemOptionVM
                {
                    Id = f.Id,
                    FocusId = f.NutritionalFocusId,
                    ItemId = f.NutritionalItemId,
                    FocusName = f.NutritionalFocus.Name,
                    ItemName = f.NutritionalItem.Name
                })
                .ToList();

            return response;
        }



        public SaveMealsTrackingResponseVM SaveMealsTracking(
     int userId,
     int tenantId,
     List<SavePendingMeals> items)
        {
            foreach (var f in items)
            {
                var date = DateOnly.Parse(f.Date);

                var mealPlan = _context.MealPlan
                    .FirstOrDefault(mp =>
                        mp.UserId == userId &&
                        mp.TenantId == tenantId &&
                        mp.Date == date &&
                        mp.MealTypeId == f.MealTypeId &&
                        !mp.IsDeleted);

                if (mealPlan == null)
                    continue;

                // ------------------------------
                //   CASE A: Planned item
                // ------------------------------
                if (f.NutritionalItemId > 0)
                {
                    int plannedQty = f.PlannedQty;
                    int consumedQty = f.ConsumedQty;

                    int monitoringQty = Math.Min(consumedQty, plannedQty);

                    var existingMonitoring = _context.MealPlanMonitoring
                        .FirstOrDefault(m =>
                            m.MealPlanId == mealPlan.Id &&
                            m.NutritionalItemId == f.NutritionalItemId &&
                            !m.IsDeleted);

                    if (existingMonitoring != null)
                    {
                        existingMonitoring.Quantity = monitoringQty;
                        existingMonitoring.UpdatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        _context.MealPlanMonitoring.Add(new MMealPlanMonitoring
                        {
                            MealPlanId = mealPlan.Id,
                            NutritionalItemId = f.NutritionalItemId,
                            Quantity = monitoringQty,
                            TenantId = tenantId,
                            CreatedBy = userId,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false
                        });
                    }

                    // EXTRA (unplanned)
                    int extraQty = consumedQty - plannedQty;

                    if (extraQty > 0)
                    {
                        var existingUnplanned = _context.UnplannedMeals
                            .FirstOrDefault(u =>
                                u.MealPlanId == mealPlan.Id &&
                                u.NutritionalItemId == f.NutritionalItemId &&
                                !u.IsDeleted);

                        if (existingUnplanned != null)
                        {
                            existingUnplanned.Quantity += extraQty;
                            existingUnplanned.UpdatedOn = DateTime.UtcNow;
                        }
                        else
                        {
                            _context.UnplannedMeals.Add(new MUnplannedMeal
                            {
                                MealPlanId = mealPlan.Id,
                                NutritionalItemId = f.NutritionalItemId,
                                Quantity = extraQty,
                                OtherName = null,
                                OtherCaloriesQuantity = 0,
                                TenantId = tenantId,
                                CreatedBy = userId,
                                CreatedOn = DateTime.UtcNow,
                                IsDeleted = false
                            });
                        }
                    }
                }

                // ------------------------------
                //   CASE B: Custom (new) item
                // ------------------------------
                else
                {
                    var existingCustom = _context.UnplannedMeals
                        .FirstOrDefault(u =>
                            u.MealPlanId == mealPlan.Id &&
                            u.NutritionalItemId == 0 &&
                            u.OtherName == f.OtherName &&
                            !u.IsDeleted);

                    if (existingCustom != null)
                    {
                        existingCustom.Quantity += f.ConsumedQty;
                        existingCustom.UpdatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        _context.UnplannedMeals.Add(new MUnplannedMeal
                        {
                            MealPlanId = mealPlan.Id,
                            NutritionalItemId = 0,
                            OtherName = f.OtherName,
                            OtherCaloriesQuantity = f.OtherCaloriesQuantity,
                            Quantity = f.ConsumedQty,
                            TenantId = tenantId,
                            CreatedBy = userId,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false
                        });
                    }
                }
            }

            _context.SaveChanges();

            return new SaveMealsTrackingResponseVM
            {
                Success = true,
                Message = "Meal tracking saved successfully"
            };
        }


    }
}
