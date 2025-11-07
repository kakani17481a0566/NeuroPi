namespace NeuroPi.Nutrition.ViewModel.UserMealType
{
    public class UserMealTypeUpdateVM
    {
        public int UserId { get; set; }

        public int MealTypeId { get; set; }

        public string PreferredMealTime { get; set; }

        public int TenantId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
