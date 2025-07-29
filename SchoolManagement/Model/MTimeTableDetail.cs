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
        [ForeignKey(nameof(Period))]
        public int PeriodId { get; set; }
        public virtual MPeriod Period { get; set; }

        [Column("subject_id")]
        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [Column("time_table_id")]
        [ForeignKey(nameof(TimeTable))]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }

        [Column("teacher_id")]
        [ForeignKey(nameof(Teacher))]
        public int TeacherId { get; set; }
        public virtual MUser Teacher { get; set; }

    
    }
}
