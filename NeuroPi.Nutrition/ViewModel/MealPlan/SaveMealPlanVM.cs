using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel
{
    public class SaveMealPlanVM
    {
        public int UserId { get; set; }
        public DateOnly Date { get; set; }
        public int TenantId { get; set; }

        public List<MealPlanItemVM> Items { get; set; } = new();

        // ⭐ Convert entire request to model list
        public List<MMealPlan> ToModelList()
        {
            return Items.Select(i => new MMealPlan
            {
                UserId = this.UserId,
                TenantId = this.TenantId,
                Date = this.Date,
                MealTypeId = i.MealTypeId,
                NutritionalItemId = i.NutritionalItemId,
                Quantity = i.Qty,
                CreatedBy = this.UserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            }).ToList();
        }
    }

    public class MealPlanItemVM
    {
        public int MealTypeId { get; set; }
        public int NutritionalItemId { get; set; }
        public int Qty { get; set; }
    }

    public class SaveMealPlanResponseVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateOnly Date { get; set; }
        public List<SavedMealItemVM> SavedItems { get; set; } = new();
    }

    public class SavedMealItemVM
    {
        public int MealTypeId { get; set; }
        public int NutritionalItemId { get; set; }
        public int Qty { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsInserted { get; set; }
    }
}
