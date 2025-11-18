namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaloriesQuantity { get; set; }
        public int ProteinQuantity { get; set; }
        public List<int> MealTypeIds { get; set; }
        public List<int> FocusIds { get; set; }
        public List<int> VitaminIds { get; set; }
        public int? DietTypeId { get; set; }
        public string? ItemImage { get; set; } = null;
        public bool UserFavourite { get; set; }
    }
    public class MealPlan
    {
        public int MealTypeId { get; set; }
        //public DateOnly Date { get; set; }
        public List<Items> Items { get; set; }
    }
    public class Items
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int CaloriesQuantity { get; set; }
        public int Type { get; set; }
        public int ProteinQuantity { get; set; }
        //public List<int> MealTypeIds { get; set; }
        public List<int> FocusIds { get; set; }
        public List<int> VitaminIds { get; set; }
        public int DietTypeId { get; set; }
        public string? ItemImage { get; set; } = null;
        public bool UserFavourite { get; set; }

    }
    public class Filters
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }





}
