namespace NeuroPi.Nutrition.ViewModel.UserGene
{
    public class UserGeneUpdateVM
    {
        public int UserId { get; set; }
        public int GeneId { get; set; }
        public string GeneStatus { get; set; }
        public string Notes { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
