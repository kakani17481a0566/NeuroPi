using NeuroPi.Nutrition.Model;
 

namespace NeuroPi.Nutrition.ViewModel.NutritionalFocus
{
    public class NutritionalFocusResponseVM
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public static NutritionalFocusResponseVM ToViewModel(MNutritionalFocus model)
        {
            return new NutritionalFocusResponseVM
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<NutritionalFocusResponseVM> ToViewModelList(List<MNutritionalFocus> modelList)
        {
            List<NutritionalFocusResponseVM> result = new List<NutritionalFocusResponseVM>();
            foreach (var model in modelList)
            {
                result.Add(ToViewModel(model));
            }
            ;
            return result;
        }
   
    }
}
