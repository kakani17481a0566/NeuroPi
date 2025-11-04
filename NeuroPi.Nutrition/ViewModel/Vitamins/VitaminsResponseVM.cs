using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Model;


namespace NeuroPi.Nutrition.ViewModel.Vitamins
{
    public class VitaminsResponseVM 
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

        public static VitaminsResponseVM ToViewModel (MVitamins model)
        {
            return new VitaminsResponseVM
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

        public static List<VitaminsResponseVM> ToViewModelList (List<MVitamins> modelList)
        {
            return modelList.Select(m => ToViewModel(m)).ToList();
        }

       
    }
}
