using NeuroPi.Nutrition.Model;
using System.Security.Cryptography.X509Certificates;

namespace NeuroPi.Nutrition.ViewModel.NutritionalItem
{
    public class NutritionalItemResponseVM
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CaloriesQuantity { get; set; }

        public int ProteinQuantity { get; set; }

        public int Quantity { get; set; }

        public bool Receipe { get; set; }

        public int NutritionalItemTypeId { get; set; }

        public bool Edible { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static NutritionalItemResponseVM ToViewModel(MNutritionalItem model)
        {
            return new NutritionalItemResponseVM
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                CaloriesQuantity = model.CaloriesQuantity,
                ProteinQuantity = model.ProteinQuantity,
                Quantity = model.Quantity,
                Receipe = model.Receipe,
                NutritionalItemTypeId = model.NutritionalItemTypeId,
                Edible = model.Edible,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn,

            };
        }
        public static List<NutritionalItemResponseVM> ToViewModelList(List<MNutritionalItem> list)
        {
            return list.Select(x => ToViewModel(x)).ToList();
        }
    }
}