using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("time_table_worksheets")]
    public class MTimeTableWorksheet : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("time_table_id")]
        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [Required]
        [Column("worksheet_id")]
        [ForeignKey("Worksheet")]
        public int WorksheetId { get; set; }
        public virtual MWorksheet Worksheet { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
