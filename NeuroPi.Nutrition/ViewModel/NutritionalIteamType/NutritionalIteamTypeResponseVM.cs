using NeuroPi.Nutrition.Model;
 
namespace NeuroPi.Nutrition.ViewModel.NutritionalIteamType
{
    public class NutritionalIteamTypeResponseVM
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public static NutritionalIteamTypeResponseVM ToViewModel(MNutritionalItemType model)=> new NutritionalIteamTypeResponseVM
        {
            
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
           
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        

        public static List<NutritionalIteamTypeResponseVM> ToViewModelList(List<MNutritionalItemType> modelList)
        {
            List<NutritionalIteamTypeResponseVM> result = new List<NutritionalIteamTypeResponseVM>();
            foreach (var model in modelList)
            {
                result.Add(ToViewModel(model));
            }
            ;
            return result;
        }

    }
}
