using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.MealType
{
    public class MealTypeResponseVM
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

        public static MealTypeResponseVM ToViewModel(MMealType model)
        {
            return new MealTypeResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<MealTypeResponseVM> ToViewModelList(List<MMealType> models)
        {
            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
