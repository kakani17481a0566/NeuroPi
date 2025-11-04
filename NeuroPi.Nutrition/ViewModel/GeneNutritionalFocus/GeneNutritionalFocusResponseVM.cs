using NeuroPi.Nutrition.Model;


namespace NeuroPi.Nutrition.ViewModel
{
    public class GeneNutritionalFocusResponseVM
    {
        public int Id { get; set; }

        public int NutritionalFocusId { get; set; }

        public int GenesId { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        //public virtual ICollection<NutritionalFocusResponseVM> NutritionalFocus { get; set; } = new List<NutritionalFocusResponseVM>();
        //public virtual ICollection<GenGenesResponseVM> Genes { get; set; } = new List<GenGenesResponseVM>();

        public static GeneNutritionalFocusResponseVM ToViewModel(MGeneNutritionalFocus model) => new GeneNutritionalFocusResponseVM
        {
            Id = model.Id,
            NutritionalFocusId = model.NutritionalFocusId,
            GenesId = model.GenesId,

            TenantId = model.TenantId,
            CreatedBy = model.CreatedBy,
            CreatedOn = model.CreatedOn,
            UpdatedBy = model.UpdatedBy,
            UpdatedOn = model.UpdatedOn
        };

        public static List<GeneNutritionalFocusResponseVM> ToViewModelList(List<MGeneNutritionalFocus> modelList)
        {
            List<GeneNutritionalFocusResponseVM> result = new List<GeneNutritionalFocusResponseVM>();
            foreach (var model in modelList)
            {
                result.Add(ToViewModel(model));
            }
            ;
            return result;
        }
    }
}
