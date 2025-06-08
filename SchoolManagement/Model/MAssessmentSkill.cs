using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("assessment_skills")]
    public class MAssessmentSkill : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Subject))]
        [Column("subject_id")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [Required]
        [ForeignKey(nameof(Tenant))]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
