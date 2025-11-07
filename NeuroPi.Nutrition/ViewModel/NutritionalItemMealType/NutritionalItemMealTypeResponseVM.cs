using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemMealType
{
    public class NutritionalItemMealTypeResponseVM
    {
        public int Id { get; set; }

        public int NutritionalItemId { get; set; }

        public int MealTypeId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static NutritionalItemMealTypeResponseVM ToViewModel(MNutritionalItemMealType model)
        {
            return new NutritionalItemMealTypeResponseVM
            {
                Id = model.Id,
                NutritionalItemId = model.NutritionalItemId,
                MealTypeId = model.MealTypeId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn,

            };

        }
        public static List<NutritionalItemMealTypeResponseVM> ToViewModelList(List<MNutritionalItemMealType> modelList)
        {
            return modelList.Select(model => ToViewModel(model)).ToList();
        }
    }
}
