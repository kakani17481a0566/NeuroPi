using NeuroPi.Nutrition.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model.Nutrition
{
    [Table("nut_user_games_status", Schema = "nutrition")]
    public class MUserGamesStatus : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("recording_url")]
        public string? RecordingUrl { get; set; }

        [Column("nut_timetable_id")]
        public int? NutTimetableId { get; set; }

        [ForeignKey(nameof(NutTimetableId))]
        public MTimetable? Timetable { get; set; }

        [Column("activity_date")]
        public DateTime? ActivityDate { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public MNutritionMaster? Status { get; set; } // from nut_master

        [Column("tenant_id")]
        public int? TenantId { get; set; }
    }
}
