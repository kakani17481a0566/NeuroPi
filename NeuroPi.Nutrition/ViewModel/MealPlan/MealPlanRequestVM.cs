using NeuroPi.Nutrition.Model;


namespace NeuroPi.Nutrition.ViewModel
{
    public class MealPlanRequestVM
    {             
        public int UserId { get; set; }       
        public int MealTypeId { get; set; }       
        public DateOnly Date { get; set; }        
        public int NutritionalItemId { get; set; }      
        public int Quantity { get; set; }      
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public static MMealPlan ToModel(MealPlanRequestVM model)
        {
            return new MMealPlan
            {
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

    }
}
