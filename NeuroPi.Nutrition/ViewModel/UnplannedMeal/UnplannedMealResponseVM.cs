using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UnplannedMeal
{
    public class UnplannedMealResponseVM
    {
        public int Id { get; set; }

        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public string OtherName { get; set; }

        public int OtherCaloriesQuantity { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static UnplannedMealResponseVM ToViewModel(MUnplannedMeal model)
        {
            if (model == null) return null;
            return new UnplannedMealResponseVM
            {
                Id = model.Id,
                MealPlanId = model.MealPlanId,
                NutritionalItemId = model.NutritionalItemId,
                Quantity = model.Quantity,
                OtherName = model.OtherName,
                OtherCaloriesQuantity = model.OtherCaloriesQuantity,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<UnplannedMealResponseVM> ToViewModelList(List<MUnplannedMeal> modelList)
        {
            return modelList.Select(model => ToViewModel(model)).ToList();
        }
    }
}
