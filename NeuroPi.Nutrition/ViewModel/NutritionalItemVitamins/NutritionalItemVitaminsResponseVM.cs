using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins
{
    public class NutritionalItemVitaminsResponseVM
    {
        public int Id { get; set; }
        public int NutritionalItemId { get; set; }
        public int VitaminsId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static NutritionalItemVitaminsResponseVM ToViewModel(MNutritionalItemVitamins model)
        {
            return new NutritionalItemVitaminsResponseVM
            {
                Id = model.Id,
                NutritionalItemId = model.NutritionalItemId,
                VitaminsId = model.VitaminsId,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }
        public static List<NutritionalItemVitaminsResponseVM> ToViewModelList(List<MNutritionalItemVitamins> modelList)
        {
            return modelList.Select(model => ToViewModel(model)).ToList();
        }
    }
}
