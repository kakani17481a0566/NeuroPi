namespace NeuroPi.Nutrition.ViewModel.UserGamesStatus
{
    public class UserGameStatusVM
    {
        public int Id { get; set; }                 // Auto-generated PK
        public int UserId { get; set; }
        public int TimetableId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int StatusId { get; set; }

        public string? StatusCode { get; set; }     // e.g., COMPLETED
        public string? StatusName { get; set; }     // e.g., Completed

        public string? RecordingUrl { get; set; }   // video/image proof
    }
}
