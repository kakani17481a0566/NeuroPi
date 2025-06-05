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

     
        [Column("time_table_id")]
        public int TimeTableId { get; set; }
       

        [Column("worksheet_id")]
        public int WorksheetId { get; set; }
       

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
