using NeuroPi.UserManagment.Model;
using System;
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

        [Required]
        [Column("topic_id")]
        public int TopicId { get; set; }

        [ForeignKey("TopicId")]
        public virtual MTopic Topic { get; set; }

        [Required]
        [Column("assessment_skill_id")]
        public int AssessmentSkillId { get; set; }

        [ForeignKey("AssessmentSkillId")]
        public virtual MAssessmentSkill AssessmentSkill { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }

        
    }
}
