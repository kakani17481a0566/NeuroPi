namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalDropdownsVM
    {
        public List<Filters> MealTypes { get; set; } = new();
        public List<Filters> Vitamins { get; set; } = new();
        public List<Filters> FocusTags { get; set; } = new();
        public List<Filters> ItemTypes { get; set; } = new();

    }
}
