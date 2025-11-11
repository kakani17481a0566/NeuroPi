using NeuroPi.Nutrition.Model;

namespace NeuroPi.Nutrition.ViewModel.UserGene
{
    public class UserGeneRequestVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GeneId { get; set; }
        public string GeneStatus { get; set; }

        public string Notes { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MUserGene ToModel(UserGeneRequestVM viewModel)
        {
            return new MUserGene
            {
                UserId = viewModel.UserId,
                GeneId = viewModel.GeneId,
                GeneStatus = viewModel.GeneStatus,
                Notes = viewModel.Notes,
                TenantId = viewModel.TenantId,
                CreatedBy = viewModel.CreatedBy,
                CreatedOn = viewModel.CreatedOn
            };
        }
    }
}
