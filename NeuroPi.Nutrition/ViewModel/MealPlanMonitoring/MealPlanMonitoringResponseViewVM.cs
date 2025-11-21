using System;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    // ============================================================
    // 📌 MAIN RESPONSE
    // ============================================================
    public class MealPlanMonitoringResponseViewVM
    {
        public DateOnly Date { get; set; }

        // Today's or Pending day's full section list
        public List<MealWindowVM> Sections { get; set; } = new();

        // Focus achieved today (optional)
        public List<FocusVM> AchievedFocus { get; set; } = new();

        // Total calories for THIS DATE
        public int TotalCalories { get; set; }

        // Missed days (contains full card info)
        public MissedDaysInfoVM MissedDays { get; set; } = new();

        // Sent to frontend for filter usage
        public List<FilterOptions> AllMealTypes { get; set; } = new();
        public List<FilterOptions> AllFoods { get; set; } = new();
        public List<FilterOptions> AllFocus { get; set; } = new();
        public List<FilterOptions> AllVitamins { get; set; } = new();
    }


    // ============================================================
    // 🍽️ MEAL WINDOW (Breakfast, Lunch, Snack, Dinner…)
    // ============================================================
    public class MealWindowVM
    {
        public int MealTypeId { get; set; }
        public string MealTypeName { get; set; }
        public string Time { get; set; }

        public int SectionCalories { get; set; }

        public List<FoodMonitorVM> Items { get; set; } = new();
    }


    // ============================================================
    // 🍎 FOOD ITEM IN A MEAL WINDOW
    // ============================================================
    public class FoodMonitorVM
    {
        public int ItemId { get; set; }
        public string Title { get; set; }

        public string Unit { get; set; }
        public string ItemImage { get; set; }

        public int Kcal { get; set; }

        public int PlannedQty { get; set; }
        public int ConsumedQty { get; set; }

        public bool IsUnplanned { get; set; }
    }


    // ============================================================
    // 🔖 FOCUS TAGS (Energy, Detox, Protein…)
    // ============================================================
    public class FocusVM
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }


    // ============================================================
    // 📅 MISSED DAYS SUMMARY (with full cards)
    // ============================================================
    public class MissedDaysInfoVM
    {
        public int TotalMissedDays { get; set; }
        public int StreakMissed { get; set; }

        public DateOnly? LastEatenDate { get; set; }

        // List of full PENDING cards (same as today's card)
        public List<PreviousDayVM> History { get; set; } = new();
    }


    // ============================================================
    // 📅 PENDING DAY CARD (structured exactly like TODAY)
    // ============================================================
    public class PreviousDayVM
    {
        public DateOnly Date { get; set; }
        public string Status { get; set; }

        // Same structure as Today: Breakfast / Lunch / Snack / Dinner
        public List<MealWindowVM> Sections { get; set; } = new();

        // Total calories for this date
        public int TotalCalories { get; set; }
    }


    // ============================================================
    // 🧭 FILTER OPTIONS (meal types, foods, vitamins…)
    // ============================================================
    public class FilterOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Image { get; set; }
    }
}
