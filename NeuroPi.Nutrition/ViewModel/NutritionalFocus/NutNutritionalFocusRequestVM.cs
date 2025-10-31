namespace NeuroPi.Nutrition.ViewModel.NutritionalFocus
{
    public class NutNutritionalFocusRequestVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
