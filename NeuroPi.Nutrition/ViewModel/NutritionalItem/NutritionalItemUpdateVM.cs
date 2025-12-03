namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemUpdateVM
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CaloriesQuantity { get; set; }

        public int ProteinQuantity { get; set; }

        public int Quantity { get; set; }

        public bool Receipe { get; set; }

        public int NutritionalItemTypeId { get; set; }

        public bool Edible { get; set; }

        public string? ItemImage { get; set; } = null;

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
