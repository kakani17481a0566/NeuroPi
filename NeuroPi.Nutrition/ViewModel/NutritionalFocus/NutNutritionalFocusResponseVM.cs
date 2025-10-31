namespace NeuroPi.Nutrition.ViewModel.NutritionalFocus
{
    public class NutNutritionalFocusResponseVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
