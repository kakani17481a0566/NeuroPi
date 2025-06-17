using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("assessments")]
    public class MAssessment : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

  
        [ForeignKey(nameof(Topic))]
        [Column("topic_id")]
        public int? TopicId { get; set; }
        public virtual MTopic? Topic { get; set; }

        [Required]
        [ForeignKey(nameof(AssessmentSkill))]
        [Column("assessment_skill_id")]
        public int AssessmentSkillId { get; set; }
        public virtual MAssessmentSkill AssessmentSkill { get; set; }

        [Required]
        [ForeignKey(nameof(Tenant))]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
