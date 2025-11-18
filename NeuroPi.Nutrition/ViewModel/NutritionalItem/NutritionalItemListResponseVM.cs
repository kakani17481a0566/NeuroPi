namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemListResponseVM
    {
        public List<NutritionalItemDetailsVM> AllItems { get; set; }

        public List<MealPlan> MealPlans { get; set; }
        public DateOnly MealPlansDate { get; set; }  

        public List<Filters> MealTypes { get; set; }

        public List<Filters> ItemTypes { get; set; }

        public List<Filters> FocusTags { get; set; }

        public List<Filters> Vitamins { get; set; }

    }
}
