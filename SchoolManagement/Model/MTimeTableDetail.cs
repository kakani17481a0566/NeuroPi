using NeuroPi.UserManagment.Model;
using NodaTime;
using System;
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
        public int PeriodId { get; set; }
    

        [Column("subject_id")]
        public int SubjectId { get; set; }
     

        [Column("time_table_id")]
        public int TimeTableId { get; set; }
        
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
