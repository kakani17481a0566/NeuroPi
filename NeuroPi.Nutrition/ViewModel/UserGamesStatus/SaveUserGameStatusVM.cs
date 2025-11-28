namespace NeuroPi.Nutrition.ViewModel.UserGamesStatus
{
    public class SaveUserGameStatusVM
    {
        public int UserId { get; set; }
        public int TimetableId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int TenantId { get; set; }

        // From nutrition_master (Game Status)
        public int StatusId { get; set; }

        // Optional evidence file (video/image)
        public string? RecordingUrl { get; set; }
    }
}
