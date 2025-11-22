using System;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    // ============================================================
    // 📌 MAIN MONITOR RESPONSE
    // ============================================================
    public class MealPlanMonitoringResponseViewVM
    {
        /// <summary>Selected date (today or requested)</summary>
        public DateOnly Date { get; set; }

        /// <summary>Breakfast / Lunch / Snack / Dinner sections with items</summary>
        public List<MealWindowVM> Sections { get; set; } = new();

        /// <summary>Focus tags achieved based on consumed items</summary>
        public List<FocusOptionVM> AchievedFocus { get; set; } = new();

        /// <summary>Total calories consumed for the selected date</summary>
        public int TotalCalories { get; set; }

        /// <summary>Pending days summary and history</summary>
        public MissedDaysInfoVM MissedDays { get; set; } = new();

        // -------------------- MASTER DATA --------------------

        /// <summary>All meal types (Breakfast, Lunch...)</summary>
        public List<MealTypeOptionVM> AllMealTypes { get; set; } = new();

        /// <summary>All nutritional items</summary>
        public List<FoodOptionVM> AllFoods { get; set; } = new();

        /// <summary>All focus tags</summary>
        public List<FocusOptionVM> AllFocus { get; set; } = new();

        /// <summary>All vitamins list</summary>
        public List<VitaminOptionVM> AllVitamins { get; set; } = new();

        /// <summary>Focus-Item mapping (detox → item IDs)</summary>
        public List<FocusItemOptionVM> AllFocusItems { get; set; } = new();
    }

    // ============================================================
    // 🍽️ MEAL WINDOW (Breakfast / Lunch / Snack / Dinner)
    // ============================================================
    public class MealWindowVM
    {
        public int MealTypeId { get; set; }
        public string MealTypeName { get; set; }
        public string Time { get; set; }

        /// <summary>Total calories for this section</summary>
        public int SectionCalories { get; set; }

        /// <summary>List of items inside this meal window</summary>
        public List<FoodMonitorVM> Items { get; set; } = new();
    }

    // ============================================================
    // 🍎 FOOD ITEM INSIDE A MEAL
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

        /// <summary>If true → came from unplanned meals</summary>
        public bool IsUnplanned { get; set; }
    }

    // ============================================================
    // 📅 MISSED DAYS SUMMARY
    // ============================================================
    public class MissedDaysInfoVM
    {
        /// <summary>Total number of pending (not monitored) days</summary>
        public int TotalMissedDays { get; set; }

        /// <summary>Continuous streak of missed days (yesterday, day before…)</summary>
        public int StreakMissed { get; set; }

        /// <summary>Last date where the user recorded consumption</summary>
        public DateOnly? LastEatenDate { get; set; }

        /// <summary>Pending history cards</summary>
        public List<PreviousDayVM> History { get; set; } = new();
    }

    // ============================================================
    // 📅 FULL PENDING DAY CARD (History)
    // ============================================================
    public class PreviousDayVM
    {
        public DateOnly Date { get; set; }
        public string Status { get; set; }

        public List<MealWindowVM> Sections { get; set; } = new();

        /// <summary>Total planned calories for the pending day</summary>
        public int TotalCalories { get; set; }
    }

    // ============================================================
    // 🧭 MASTER OPTION MODELS
    // ============================================================

    // 🥗 MEAL TYPES
    public class MealTypeOptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>Optional description (e.g., Morning meal)</summary>
        public string? Time { get; set; }
    }

    // 🥑 FOODS
    public class FoodOptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int Kcal { get; set; }
        public string Unit { get; set; }
    }

    // 🎯 FOCUS TAG (Detox, Weight Loss…)
    public class FocusOptionVM
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }

    // 💊 VITAMINS
    public class VitaminOptionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // 🔗 FOCUS-ITEM LINK TABLE (nut_focus_item)
    public class FocusItemOptionVM
    {
        public int Id { get; set; }
        public int FocusId { get; set; }
        public int ItemId { get; set; }

        public string FocusName { get; set; }
        public string ItemName { get; set; }
    }
}
