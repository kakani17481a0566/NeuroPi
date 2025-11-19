namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    public class MealPlan7daysCardVM
    {
        public List<MealPlan7DayItemVM> Days { get; set; } = new();
        public UnlockNoteVM UnlockNote { get; set; }
    }

    public class MealPlan7DayItemVM
    {
        public int Date { get; set; }
        public string Month { get; set; }
        public string Weekday { get; set; }

        public string FullDate { get; set; }  // ⭐ NEW (ISO yyyy-MM-dd)

        public int Calories { get; set; }
        public int Protein { get; set; }

        public Dictionary<string, int> MealWiseCalories { get; set; }
        public Dictionary<string, int> MealWiseProtein { get; set; }

        public string StatusText { get; set; }
        public string StatusType { get; set; }
    }


    public class UnlockNoteVM
    {
        public bool Enabled { get; set; }
        public string TextTop { get; set; }
        public string TextBottom { get; set; }
    }
}
