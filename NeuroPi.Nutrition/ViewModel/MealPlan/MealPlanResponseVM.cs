using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel
{
    public class MealPlanResponseVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MealTypeId { get; set; }
        public DateOnly Date { get; set; }
        public int NutritionalItemId { get; set; }
        public int Quantity { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public static MealPlanResponseVM ToViewModel(MMealPlan model)
        {
            return new MealPlanResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                MealTypeId = model.MealTypeId,
                Date = model.Date,
                NutritionalItemId = model.NutritionalItemId,
                Quantity = model.Quantity,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn
            };
        }

        public static List<MealPlanResponseVM> ToViewModelList(List<MMealPlan> mealplanslist)
        { 
            return mealplanslist.Select(mp => ToViewModel(mp)).ToList();
        }

    }
}
