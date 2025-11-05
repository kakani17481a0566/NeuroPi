namespace NeuroPi.Nutrition.ViewModel.NutritionMaster
{
    public class NutritionMasterUpdateVM
    {
        public string name { get; set; }

        public int NutritionMasterTypeId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
