using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("time_table_assessments")]
    public class MTimeTableAssessment : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("time_table_id")]
        public int TimeTableId { get; set; }

        [Column("assessment_id")]
        public int AssessmentId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey(nameof(AssessmentId))]
        public virtual MAssessment Assessment { get; set; }
    }
}
