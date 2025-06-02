using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolManagement.Model
{
    public class MAssessment : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public virtual MTopic Topic { get; set; }

        [ForeignKey("AssessmentSkill")]
        public int AssessmentSkillId { get; set; }
        public virtual MAssessmentSkill AssessmentSkill { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
