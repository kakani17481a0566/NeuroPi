using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.MealType
{
    public class MealTypeRequestVM
    {
        
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MMealType ToModel(MealTypeRequestVM model)
        {
            return new MMealType
            {
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn
            };
        }
    }
}
