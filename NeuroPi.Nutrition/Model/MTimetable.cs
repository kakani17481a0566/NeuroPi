using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model.Nutrition
{
    [Table("nut_timetable", Schema = "nutrition")]
    public class MTimetable : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("resource_id")]
        public int? ResourceId { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public MResourceMaster? Resource { get; set; }

        [Column("cycle")]
        public string? Cycle { get; set; }

        [Column("duration")]
        public string? Duration { get; set; }

        [Column("daily_weekly")]
        public bool? DailyWeekly { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        // Navigation
        public ICollection<MUserGamesStatus> UserStatuses { get; set; }
    }
}
