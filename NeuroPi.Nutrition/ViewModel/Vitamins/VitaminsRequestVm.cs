using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.Vitamins
{
    public class VitaminsRequestVm
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MVitamins ToModel(VitaminsRequestVm model)
        {
            return new MVitamins
            {
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn
            };
        }
    }
}

    

