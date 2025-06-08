using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("time_table_detail")]
    public class MTimeTableDetail : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("period_id")]
        [ForeignKey("Period")]
        public int PeriodId { get; set; }
        public virtual MPeriod Period { get; set; }

        [Column("subject_id")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [Column("time_table_id")]
        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
