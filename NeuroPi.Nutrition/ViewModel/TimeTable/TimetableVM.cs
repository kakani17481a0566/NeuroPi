using System;
using System.Collections.Generic;

namespace NeuroPi.Nutrition.ViewModel.TimeTable
{
    public class TimetableVM
    {
        // ---------------------------------------------------------
        // 🟦 Timetable
        // ---------------------------------------------------------
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public string? Cycle { get; set; }          // NEW
        public string? Duration { get; set; }       // NEW
        public bool? DailyWeekly { get; set; }      // NEW

        // ---------------------------------------------------------
        // 🟩 Resource (nut_resource_master)
        // ---------------------------------------------------------
        public int? ResourceId { get; set; }
        public string? ResourceName { get; set; }
        public string? ResourceShortName { get; set; }
        public string? ResourceDescription { get; set; }
        public string? PreviewUrl { get; set; }
        public string? Image { get; set; }
        public string? Script { get; set; }
        public string? ResourceType { get; set; }

        public List<string> Instructions { get; set; } = new();

        // ---------------------------------------------------------
        // 🟧 User Status
        // ---------------------------------------------------------
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? StatusCode { get; set; }
    }
}
