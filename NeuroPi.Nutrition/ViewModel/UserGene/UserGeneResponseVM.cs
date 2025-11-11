using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.ViewModel.UserGene;

namespace NeuroPi.Nutrition.ViewModel.UserGene
{
    public class UserGeneResponseVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GeneId { get; set; }
        public string GeneStatus { get; set; }

        public string Notes { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static UserGeneResponseVM ToViewModel(MUserGene model)
        {
            if (model == null) return null;
            return new UserGeneResponseVM
            {
                Id = model.Id,
                UserId = model.UserId,
                GeneId = model.GeneId,
                GeneStatus = model.GeneStatus,
                Notes = model.Notes,
                TenantId = model.TenantId,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<UserGeneResponseVM> ToViewModelList(List<MUserGene> models) => models.Select(m => ToViewModel(m)).ToList();
        
    }
}
