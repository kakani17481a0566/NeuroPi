using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.ViewModel.Common;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemResponseVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CaloriesQuantity { get; set; }
        public int ProteinQuantity { get; set; }
        public int Quantity { get; set; }
        public bool Receipe { get; set; }
        public int NutritionalItemTypeId { get; set; }
        public bool Eatble { get; set; }
        public string? ItemImage { get; set; }

        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public List<Filters> MealTypes { get; set; } = new();
        public List<Filters> Vitamins { get; set; } = new();
        public List<Filters> FocusTags { get; set; } = new();

        public static NutritionalItemResponseVM ToViewModel(MNutritionalItem model)
        {
            return new NutritionalItemResponseVM
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                CaloriesQuantity = model.CaloriesQuantity,
                ProteinQuantity = model.ProteinQuantity,
                Quantity = model.Quantity,
                Receipe = model.Receipe,
                NutritionalItemTypeId = model.NutritionalItemTypeId,
                Eatble = model.Eatble,
                ItemImage = model.ItemImage,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<NutritionalItemResponseVM> ToViewModelList(List<MNutritionalItem> list)
        {
            return list.Select(x => ToViewModel(x)).ToList();
        }
    }
}
