using NeuroPi.Nutrition.Model;
using System.Security.Cryptography.X509Certificates;

namespace NeuroPi.Nutrition.ViewModel.MealPlanMonitoring
{
    public class MealPlanMonitoringResponseVM
    {
        public int Id { get; set; }

        public int MealPlanId { get; set; }

        public int NutritionalItemId { get; set; }

        public int Quantity { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; } 

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }


        public static MealPlanMonitoringResponseVM ToViewModel(MMealPlanMonitoring model)
        {
            return new MealPlanMonitoringResponseVM
            {
                Id = model.Id,
                MealPlanId = model.MealPlanId,
                NutritionalItemId = model.NutritionalItemId,
                Quantity = model.Quantity,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn

            };

        }

        public static List<MealPlanMonitoringResponseVM> ToViewModelList(List<MMealPlanMonitoring> list)
        {

            return list.Select(m => ToViewModel(m)).ToList();
        }
    }
}
