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




    }
}
