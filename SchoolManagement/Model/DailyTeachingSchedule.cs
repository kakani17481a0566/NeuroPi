using System.ComponentModel.DataAnnotations.Schema; // Required for [Column] attribute

namespace SchoolManagement.Model
{
    // Important: This model maps to a SQL View, not a table.
    // It will be configured as a Keyless Entity in DbContext.



    [Table("daily_teaching_schedule")]
    public class DailyTeachingSchedule
    {
        // Maps to the 'day_of_week' column from the view
        public string DayOfWeek { get; set; }

        // Maps to the "Period 1" column from the view.
        // Use [Column("Period 1")] because of the space in the SQL column name.
        [Column("Period 1")]
        public string Period1 { get; set; }

        // Maps to the "Period 2" column from the view
        [Column("Period 2")]
        public string Period2 { get; set; }

        // Maps to the "Period 3" column from the view
        [Column("Period 3")]
        public string Period3 { get; set; }

        // Maps to the "Period 4" column from the view
        [Column("Period 4")]
        public string Period4 { get; set; }

        // Maps to the "Period 5" column from the view
        [Column("Period 5")]
        public string Period5 { get; set; }

        // Add more properties here if you add more Period columns to your SQL view
        // For example:
        // [Column("Period 6")]
        // public string Period6 { get; set; }
    }
}