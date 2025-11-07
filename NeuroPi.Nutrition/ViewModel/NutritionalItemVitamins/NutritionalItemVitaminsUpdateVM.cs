namespace NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins
{
    public class NutritionalItemVitaminsUpdateVM
    {
        public int NutritionalItemId { get; set; }
        public int VitaminsId { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
