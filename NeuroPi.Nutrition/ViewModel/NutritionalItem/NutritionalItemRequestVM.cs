using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemRequestVM
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

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MNutritionalItem ToModel(NutritionalItemRequestVM vm)
        {
            return new MNutritionalItem 
            { 
                Code = vm.Code,
                Name = vm.Name,
                Description = vm.Description,
                CaloriesQuantity = vm.CaloriesQuantity,
                ProteinQuantity = vm.ProteinQuantity,
                Quantity = vm.Quantity,
                Receipe = vm.Receipe,
                NutritionalItemTypeId = vm.NutritionalItemTypeId,
                Edible = vm.Edible,
                CreatedBy = vm.CreatedBy,
                CreatedOn = vm.CreatedOn

            };

        }

    }
}
